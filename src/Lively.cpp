#include "Lively.h"

[System::STAThread]
void LivelyMain(array<System::String^>^ arguments) {
	using Application = System::Windows::Forms::Application;

	Application::EnableVisualStyles();
	Application::SetCompatibleTextRenderingDefault(false);

	Lively::Lively window = { };
	Application::Run(% window);
}
