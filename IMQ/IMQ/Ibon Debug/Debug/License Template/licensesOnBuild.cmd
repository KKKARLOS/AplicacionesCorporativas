dir .\Lib\License Template
SET FILE_TO_COPY=".\Lib\License Template\licenses.licx"

SET DESTINATION_PATH=".\Dashboard.ContentPanes\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\IB.IMQ.AdministrativeModule\IB.IMQ.AdministrativeModule\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\Infrastructure.Interface\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\Infrastructure.Layout\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\Infrastructure.Library\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\ReportingFoundationalModule\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\\SecurityModule\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\Shell\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\SII.HCD.Addin.A3\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\SII.HCD.Addin.CAM\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\SII.HCD.Addin.EtiquetasRX\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\SII.HCD.Addin.Generic\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\SII.HCD.Addin.IndraExport\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\SII.HCD.Addin.MDSSpain\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\SII.HCD.Addin.Sermepa\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\SII.HCD.Addin.Vademecum\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\SII.HCD.AdministrativeModule\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\SII.HCD.Administrator\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\SII.HCD.ApplicationModule\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\SII.HCD.Assistance.Entities\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\SII.HCD.AssistanceModule\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\SII.HCD.BackOfficeModule\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\SII.HCD.Generic.Reports\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\SII.HCD.ProtocolsModule\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
SET DESTINATION_PATH=".\SII.HCD.Reports.Milagrosa\Properties"
xcopy %FILE_TO_COPY% %DESTINATION_PATH% /y
