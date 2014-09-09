program SAMSampleUsageWS;

uses
  Forms,
  SAMSampleUsage in 'SAMSampleUsage.pas' {frmMain};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmMain, frmMain);
  Application.Run;
end.
