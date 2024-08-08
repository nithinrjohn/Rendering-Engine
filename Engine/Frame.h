#pragma once

#ifdef _WIN32
#define DLL_EXPORT extern "C" __declspec(dllexport)
#else
#define DLL_EXPORT
#endif

#include "glad/glad.h"
#include <iostream>

DLL_EXPORT bool Init();

DLL_EXPORT bool Render();

DLL_EXPORT void Resize(int width, int height);
