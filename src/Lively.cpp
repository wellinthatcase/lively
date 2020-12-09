// ----------------------------------------------- //
//		    © 2020 - All rights reserved.          // 
// ----------------------------------------------- //

#include "Lively.h"

[System::STAThread]
void LivelyMain(_In_opt_ array<System::String^>^ arguments) {
	using Application = System::Windows::Forms::Application;

	Application::EnableVisualStyles();
	Application::SetCompatibleTextRenderingDefault(false);

	Lively::Lively window = { };
	Application::Run(% window);
}
