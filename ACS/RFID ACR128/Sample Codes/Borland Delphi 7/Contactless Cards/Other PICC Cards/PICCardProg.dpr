program PICCardProg;

uses
  Forms,
  PICCProg in 'PICCProg.pas' {MainPICCProg};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TMainPICCProg, MainPICCProg);
  Application.Run;
end.
