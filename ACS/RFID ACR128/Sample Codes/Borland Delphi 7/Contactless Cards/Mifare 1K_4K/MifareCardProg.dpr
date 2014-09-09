program MifareCardProg;

uses
  Forms,
  MifareProg in 'MifareProg.pas' {MainMifareProg};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TMainMifareProg, MainMifareProg);
  Application.Run;
end.
