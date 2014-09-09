program KeyManagementWS;

uses
  Forms,
  KeyManagement in 'KeyManagement.pas' {frmMain};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmMain, frmMain);
  Application.Run;
end.
