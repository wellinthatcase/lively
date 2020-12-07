#pragma once
#pragma comment(lib, "gdi32.lib")
#pragma comment(lib, "User32.lib")

#ifndef UNICODE
#define UNICODE
#endif 

#include <Windows.h>

#ifndef LIVELY
#define LIVELY

namespace Lively {
	using namespace System;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::ComponentModel;


	public ref class Lively : public System::Windows::Forms::Form
	{
	public:
		Lively(void)
		{
			InitializeComponent();
		}

	protected:
		~Lively()
		{
			if (components) delete components;
		}

	private: System::Windows::Forms::Panel^ ConfigPanel;
	private: System::Windows::Forms::Panel^ Lively_TitleBar;

	private: System::Windows::Forms::PictureBox^ Lively_Icon;
	private: System::Windows::Forms::PictureBox^ Lively_eXit;
	private: System::Windows::Forms::PictureBox^ Lively_Minimize;

	private: System::Windows::Forms::TextBox^ Note_Title;

	private:
		System::ComponentModel::Container ^components;

		void InitializeComponent(void)
		{
			System::ComponentModel::ComponentResourceManager^ resources = (gcnew System::ComponentModel::ComponentResourceManager(Lively::typeid));

			using Panel      = System::Windows::Forms::Panel;
			using TextBox    = System::Windows::Forms::TextBox;
			using PictureBox = System::Windows::Forms::PictureBox;

			this->Lively_TitleBar = gcnew Panel();
			this->ConfigPanel     = gcnew Panel();
			this->Note_Title      = gcnew TextBox();
			this->Lively_Minimize = gcnew PictureBox();
			this->Lively_eXit     = gcnew PictureBox();
			this->Lively_Icon     = gcnew PictureBox();

			this->Lively_TitleBar->SuspendLayout();

			using ISupportInitalize = System::ComponentModel::ISupportInitialize^;

			(cli::safe_cast<ISupportInitalize>(this->Lively_eXit))    ->BeginInit();
			(cli::safe_cast<ISupportInitalize>(this->Lively_Icon))    ->BeginInit();
			(cli::safe_cast<ISupportInitalize>(this->Lively_Minimize))->BeginInit();

			this->ConfigPanel->SuspendLayout();
			this->SuspendLayout();



			this->Lively_TitleBar->TabIndex    = 0;
			this->Lively_TitleBar->Name        = L"Lively_TitleBar";
			this->Lively_TitleBar->BackColor   = System::Drawing::Color::Snow;
			this->Lively_TitleBar->Size        = System::Drawing::Size(656, 47);
			this->Lively_TitleBar->Location    = System::Drawing::Point(-3, -3);
			this->Lively_TitleBar->BorderStyle = System::Windows::Forms::BorderStyle::Fixed3D;

			this->Lively_TitleBar->Controls->Add(this->Lively_eXit);
			this->Lively_TitleBar->Controls->Add(this->Lively_Icon);
			this->Lively_TitleBar->Controls->Add(this->Lively_Minimize);

			this->Lively_TitleBar->MouseDown += gcnew System::Windows::Forms::MouseEventHandler(this, &Lively::MoveWindow);
			


			this->Lively_Minimize->TabIndex = 2;
			this->Lively_Minimize->TabStop  = false;
			this->Lively_Minimize->Name     = L"Lively_Minimize";
			this->Lively_Minimize->Size     = System::Drawing::Size(30, 16);
			this->Lively_Minimize->Location = System::Drawing::Point(572, 19);
			this->Lively_Minimize->Cursor   = System::Windows::Forms::Cursors::Hand;
			this->Lively_Minimize->Image    = (cli::safe_cast<System::Drawing::Image^>(resources->GetObject(L"Lively_Minimize.Image")));

			this->Lively_Minimize->MouseClick += gcnew System::Windows::Forms::MouseEventHandler(this, &Lively::LivelyMinimize);
			


			this->Lively_eXit->TabIndex = 1;
			this->Lively_eXit->TabStop  = false;
			this->Lively_eXit->Name     = L"Lively_eXit";
			this->Lively_eXit->Size     = System::Drawing::Size(26, 25);
			this->Lively_eXit->Location = System::Drawing::Point(608, 10);
			this->Lively_eXit->Cursor   = System::Windows::Forms::Cursors::Hand;
			this->Lively_eXit->Image    = (cli::safe_cast<System::Drawing::Image^>(resources->GetObject(L"Lively_eXit.Image")));

			this->Lively_eXit->MouseClick += gcnew System::Windows::Forms::MouseEventHandler(this, &Lively::LivelyExit);
			

			using Image = System::Drawing::Image^;

			this->Lively_Icon->TabIndex      = 0;
			this->Lively_Icon->ImageLocation = L"";
			this->Lively_Icon->TabStop       = false;
			this->Lively_Icon->Name          = L"Lively_Icon";
			this->Lively_Icon->Location      = System::Drawing::Point(3, 3);
			this->Lively_Icon->Size          = System::Drawing::Size(43, 37);
			this->Lively_Icon->Image         = (cli::safe_cast<Image>(resources->GetObject(L"Lively_Icon.Image")));
			this->Lively_Icon->InitialImage  = (cli::safe_cast<Image>(resources->GetObject(L"Lively_Icon.InitialImage")));
			


			this->ConfigPanel->Location = System::Drawing::Point(-3, 40);
			this->ConfigPanel->Name     = L"ConfigPanel";
			this->ConfigPanel->Size     = System::Drawing::Size(656, 65);
			this->ConfigPanel->TabIndex = 1;

			this->ConfigPanel->Controls->Add(this->Note_Title);
			


			this->Note_Title->TabIndex = 0;
			this->Note_Title->Name     = L"Note_Title";
			this->Note_Title->Location = System::Drawing::Point(5, 3);
			this->Note_Title->Size     = System::Drawing::Size(405, 27);
			this->Note_Title->Font     = (gcnew System::Drawing::Font(L"Segoe UI", 11.25F));
			


			this->Controls->Add(this->ConfigPanel);
			this->Controls->Add(this->Lively_TitleBar);

			this->Name                = L"Lively";
			this->Text                = L"Lively";
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->ClientSize          = System::Drawing::Size(645, 562);
			this->Cursor              = System::Windows::Forms::Cursors::Arrow;
			this->AutoScaleMode       = System::Windows::Forms::AutoScaleMode::Font;
			this->FormBorderStyle     = System::Windows::Forms::FormBorderStyle::None;
			this->Icon                = (cli::safe_cast<System::Drawing::Icon^>(resources->GetObject(L"$this.Icon")));

			this->ResumeLayout(false);
			this->ConfigPanel->PerformLayout();
			this->ConfigPanel->ResumeLayout(false);
			this->Lively_TitleBar->ResumeLayout(false);

			(cli::safe_cast<ISupportInitialize^>(this->Lively_Minimize))->EndInit();
			(cli::safe_cast<ISupportInitialize^>(this->Lively_eXit))->EndInit();
			(cli::safe_cast<ISupportInitialize^>(this->Lively_Icon))->EndInit();

		}

	private: System::Void LivelyExit(
		_In_ System::Object^ sender, 
		_In_ System::Windows::Forms::MouseEventArgs^ events
	) {
		UNREFERENCED_PARAMETER(events);
		UNREFERENCED_PARAMETER(sender);

		using MessageBoxOT = System::Windows::Forms::MessageBox;
		using DialogResult = System::Windows::Forms::DialogResult;
		using MsgBoxWIcons = System::Windows::Forms::MessageBoxIcon;
		using MsgBoxButton = System::Windows::Forms::MessageBoxButtons;
		
		DialogResult res = MessageBoxOT::Show(
			"Are you sure you wanna quit?",
			"Confirm",
			MsgBoxButton::YesNoCancel,
			MsgBoxWIcons::Question
		);

		if (res == DialogResult::Yes)
		{
			Application::Exit();
		}
	}

	private: System::Void LivelyMinimize(
		_In_ System::Object^ sender, 
		_In_ System::Windows::Forms::MouseEventArgs^ events
	) {
		UNREFERENCED_PARAMETER(sender);
		UNREFERENCED_PARAMETER(events);

		this->WindowState     = FormWindowState::Minimized;
	}

	private: System::Void MoveWindow(
		_In_ System::Object^ sender, 
		_In_ System::Windows::Forms::MouseEventArgs^ events
	) {
		UNREFERENCED_PARAMETER(sender);

		if (events->Button == System::Windows::Forms::MouseButtons::Left)
		{
			ReleaseCapture();
			SendMessage(
				static_cast<HWND>(this->Handle.ToPointer()), 
				WM_NCLBUTTONDOWN, 
				HTCAPTION, 
				NULL
			);
		}
	}
};
}
#endif
