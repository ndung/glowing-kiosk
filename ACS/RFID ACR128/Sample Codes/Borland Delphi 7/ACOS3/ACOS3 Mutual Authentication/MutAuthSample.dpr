program MutAuthSample;

uses
  Forms,
  MutAuth in 'MutAuth.pas' {MainMutAuth};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TMainMutAuth, MainMutAuth);
  Application.Run;
end.
