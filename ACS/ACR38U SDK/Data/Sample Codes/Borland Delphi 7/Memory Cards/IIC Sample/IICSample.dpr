program IICSample;

uses
  Forms,
  IIC in 'IIC.pas' {MainIIC};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TMainIIC, MainIIC);
  Application.Run;
end.
