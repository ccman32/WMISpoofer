#include "MinHook\MinHook.h"
#include "WMIHook\WMIHook.hpp"

HINSTANCE hSelf = NULL;

DWORD WINAPI Initialize(
	void*
)
{
	MH_Initialize();

	WMIHook::Initialize(
		hSelf
	);

	return ERROR_SUCCESS;
}

BOOL WINAPI DllMain(
	HINSTANCE hinstDLL,
	DWORD fdwReason,
	LPVOID
)
{
	if (
		fdwReason == DLL_PROCESS_ATTACH
		)
	{
		hSelf = hinstDLL;

		HANDLE hInitializeThread = CreateThread(
			NULL,
			0,
			&Initialize,
			NULL,
			0,
			NULL
		);

		if (
			hInitializeThread
			)
		{
			CloseHandle(
				hInitializeThread
			);

			return TRUE;
		}
	}
	else if (
		fdwReason == DLL_PROCESS_DETACH
		)
	{
		WMIHook::Uninitialize();
		MH_Uninitialize();
	}

	return FALSE;
}