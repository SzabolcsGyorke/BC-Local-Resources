codeunit 90102 "BC LRS Management"
{

    internal procedure ExportServiceXMLConfig(BCLRSService: Record "BC LRS Service")
    var
        BCLRSServiceSetupLine: Record "BC LRS Service Setup Line";
        TempBlob: Codeunit "Temp Blob";
        FileManagement: Codeunit "File Management";
        xmlDoc: XmlDocument;
        config, usersettings, roaming, Setting, Settings : XmlElement;
        Declaration: XmlDeclaration;
        XmlWriteOptions: XmlWriteOptions;
        OutStr: OutStream;
    begin
        BCLRSServiceSetupLine.SetRange("Local Service Code", BCLRSService.Code);
        if BCLRSServiceSetupLine.FindSet() then begin
            XmlDoc := XmlDocument.Create();
            // Create the Declaration
            Declaration := XmlDeclaration.Create('1.0', 'utf-8', 'yes');
            config := XmlElement.Create('configuration');
            usersettings := XmlElement.Create('userSettings');
            roaming := XmlElement.Create('Roaming');
            Settings := XmlElement.Create('BC_Print_Service.Properties.Settings');
            config.Add(usersettings);
            usersettings.Add(roaming);

            repeat
                Clear(Setting);
                Setting := XmlElement.Create(BCLRSServiceSetupLine."Setup Variable Name");
                if BCLRSServiceSetupLine."Is Sensitive" then
                    Setting.Add(XmlText.Create(BCLRSServiceSetupLine.GetSensitiveData()))
                else
                    Setting.Add(XmlText.Create(BCLRSServiceSetupLine."Setup Value"));
                Settings.Add(Setting);
            until BCLRSServiceSetupLine.Next() = 0;

            roaming.Add(Settings);
            xmlDoc.Add(config);
            XmlWriteOptions.PreserveWhitespace(true);
            TempBlob.CreateOutStream(OutStr);
            XmlDoc.WriteTo(XmlWriteOptions, OutStr);
            FileManagement.BLOBExport(TempBlob, 'settings.config', true);
        end;
    end;

    internal procedure AddDataRetentionPolicy()
    var
        BCLRSEntry: Record "BC LRS Entry";
        RetentionPolicySetup: Record "Retention Policy Setup";
        RetentionPeriod: Record "Retention Period";
        RetenPolAllowedTables: Codeunit "Reten. Pol. Allowed Tables";
    begin
        if RetenPolAllowedTables.AddAllowedTable(Database::"BC LRS Entry", BCLRSEntry.FieldNo("Entry Completed At"), 7) then begin
            RetentionPeriod.SetRange("Retention Period", RetentionPeriod."Retention Period"::"1 Week");
            RetentionPeriod.Findfirst();
            RetentionPolicySetup.Init();
            RetentionPolicySetup.Validate("Table Id", Database::"BC LRS Entry");
            RetentionPolicySetup.Validate("Apply to all records", true);
            RetentionPolicySetup.Validate("Retention Period", RetentionPeriod.Code);
            RetentionPolicySetup.Validate(Enabled, true);
            RetentionPolicySetup.Insert(true);
        end;
    end;
}