program PollingSample;

uses
  Forms,
  PollingMain in 'PollingMain.pas' {MainPolling};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TMainPolling, MainPolling);
  Application.Run;
end.
