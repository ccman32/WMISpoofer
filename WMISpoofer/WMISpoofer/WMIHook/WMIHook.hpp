#ifndef WMIHOOK_HPP
#define WMIHOOK_HPP

#include <Windows.h>

namespace WMIHook
{
	void Initialize(
		HMODULE hSelf
	);

	void Uninitialize();
};

#endif