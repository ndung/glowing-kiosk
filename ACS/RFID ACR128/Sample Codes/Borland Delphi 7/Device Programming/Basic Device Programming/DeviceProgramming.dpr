program DeviceProgramming;

uses
  Forms,
  DevProgMain in 'DevProgMain.pas' {MainDevProg};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TMainDevProg, MainDevProg);
  Application.Run;
end.
