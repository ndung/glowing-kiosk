program ACOSBinary;

uses
  Forms,
  ACOSBinProg in 'ACOSBinProg.pas' {MainACOSBin};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TMainACOSBin, MainACOSBin);
  Application.Run;
end.
