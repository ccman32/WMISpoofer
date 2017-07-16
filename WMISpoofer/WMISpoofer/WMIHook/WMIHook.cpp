#include "WMIHook.hpp"

#include <WbemIdl.h>
#include <unordered_map>

#include "..\MinHook\MinHook.h"

#define CONST_BUFFERSIZE	16384

namespace WMIHook
{
	typedef HRESULT(__stdcall* tGetFunc)(
		IWbemClassObject*,
		LPCWSTR,
		LONG,
		VARIANT*,
		CIMTYPE*,
		long*
		);

	static tGetFunc pOrgGetFunc = NULL,
		pGetFunc = NULL;

	static std::unordered_map<std::wstring, std::unordered_map<std::wstring, std::wstring>> sectionMap;

	void SpoofPropertyValue(
		IWbemClassObject* pThis,
		LPCWSTR wszName,
		VARIANTARG* pBuffer
	)
	{
		if (
			pBuffer
			&& pBuffer->vt == VT_BSTR
			)
		{
			VARIANT className;

			pGetFunc(
				pThis,
				L"__CLASS",
				0,
				&className,
				NULL,
				NULL
			);

			auto found = sectionMap.find(
				className.bstrVal
			);

			if (
				found != sectionMap.end()
				)
			{
				auto keyFound = found->second.find(
					wszName
				);

				if (
					keyFound != found->second.end()
					)
				{
					SysFreeString(pBuffer->bstrVal);
					pBuffer->bstrVal = SysAllocString(keyFound->second.c_str());
				}
			}
		}
	}

	HRESULT __stdcall hkGetFunc(
		IWbemClassObject* pThis,
		LPCWSTR wszName,
		LONG lFlags,
		VARIANT* pVal,
		CIMTYPE* pType,
		long* plFlavor
	)
	{
		HRESULT hResult = pGetFunc(
			pThis,
			wszName,
			lFlags,
			pVal,
			pType,
			plFlavor
		);

		if (
			hResult >= WBEM_S_NO_ERROR
			)
			SpoofPropertyValue(
				pThis,
				wszName,
				pVal
			);

		return hResult;
	}

	void ProcessIniSection(
		wchar_t* pSectionName,
		wchar_t* pIniFileName
	)
	{
		wchar_t wBuffer[CONST_BUFFERSIZE];
		int iCharsRead = 0;

		iCharsRead = GetPrivateProfileSectionW(
			pSectionName,
			wBuffer,
			CONST_BUFFERSIZE,
			pIniFileName
		);

		if ((0 < iCharsRead) && ((CONST_BUFFERSIZE - 2) > iCharsRead)) {
			const wchar_t* pSubstr = wBuffer;

			while (
				L'\0' != *pSubstr
				)
			{
				size_t substrLen = wcslen(
					pSubstr
				);

				const wchar_t* pPos = wcschr(
					pSubstr,
					'='
				);

				if (NULL != pPos)
				{
					wchar_t wName[256];
					wchar_t wValue[256];

					wcsncpy_s(
						wName,

						sizeof(
							wName
							),

						pSubstr, pPos - pSubstr
					);

					wcsncpy_s(
						wValue,

						sizeof(
							wValue
							),

						pPos + 1,
						substrLen - (pPos - pSubstr)
					);

					auto value = sectionMap[pSectionName][wName] = wValue;
				}

				pSubstr += (substrLen + 1);
			}
		}
	}

	void Initialize(
		HMODULE hSelf
	)
	{
		wchar_t wIniFileName[MAX_PATH];
		wchar_t* pCutoff = NULL;

		GetModuleFileNameW(
			hSelf,
			wIniFileName,
			MAX_PATH
		);

		pCutoff = wcsrchr(
			wIniFileName,
			'\\'
		);

		*(pCutoff + 1) = '\0';

		wcscat_s(
			wIniFileName,
			L"WMIS.ini"
		);

		wchar_t sectionBuffer[CONST_BUFFERSIZE];

		GetPrivateProfileSectionNamesW(
			sectionBuffer,
			CONST_BUFFERSIZE,
			wIniFileName
		);

		wchar_t* pNextSection = NULL;
		pNextSection = sectionBuffer;

		ProcessIniSection(
			pNextSection,
			wIniFileName
		);

		while (
			*pNextSection != 0x00
			)
		{
			pNextSection = pNextSection + wcslen(
				pNextSection
			) + 1;

			if (
				*pNextSection != 0x00
				)
			{
				ProcessIniSection(
					pNextSection,
					wIniFileName
				);
			}
		}

		HMODULE hFastprox = NULL;

		while (
			!hFastprox
			)
		{
			hFastprox = GetModuleHandleA(
				"fastprox.dll"
			);

			if (
				!hFastprox
				)
				Sleep(
					100
				);
		}

		if (
			hFastprox
			)
		{
			pOrgGetFunc = (tGetFunc)GetProcAddress(
				hFastprox,
#ifdef _WIN64
				"?Get@CWbemObject@@UEAAJPEBGJPEAUtagVARIANT@@PEAJ2@Z"
#else
				"?Get@CWbemObject@@UAGJPBGJPAUtagVARIANT@@PAJ2@Z"
#endif
			);

			if (
				pOrgGetFunc
				)
			{
				MH_CreateHook(
					pOrgGetFunc,
					&hkGetFunc,
					(LPVOID*)&pGetFunc
				);

				MH_EnableHook(
					pOrgGetFunc
				);
			}
		}
	}

	void Uninitialize()
	{
		if (
			pOrgGetFunc
			&& pGetFunc
			)
		{
			MH_DisableHook(
				pOrgGetFunc
			);

			MH_RemoveHook(
				pOrgGetFunc
			);
		}

		sectionMap.clear();
	}
};