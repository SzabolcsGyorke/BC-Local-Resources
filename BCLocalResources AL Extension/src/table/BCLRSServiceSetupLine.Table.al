table 90104 "BC LRS Service Setup Line"
{
    Caption = 'BC LRS Service Setup Line';
    DataClassification = ToBeClassified;

    fields
    {
        field(1; "Local Service Code"; Code[20])
        {
            Caption = 'Local Service Code';
            DataClassification = CustomerContent;
        }
        field(2; "Setup Variable Name"; Text[100])
        {
            Caption = 'Setup Variable Name';
            DataClassification = CustomerContent;
        }
        field(3; "Setup Value"; Text[500])
        {
            Caption = 'Setup Value';
            DataClassification = CustomerContent;
        }
        field(4; "Is Sensitive"; Boolean)
        {
            Caption = 'Is Sensitive';
            DataClassification = CustomerContent;
            trigger OnValidate()
            begin
                if "Is Sensitive" then begin
                    IsolatedStorage.Set("Setup Variable Name", "Setup Value", DataScope::Company);
                    "Setup Value" := storedTxt;
                end;
            end;
        }

    }
    keys
    {
        key(PK; "Local Service Code", "Setup Variable Name")
        {
            Clustered = true;
        }
    }

    internal procedure GetSensitiveData() retval: Text
    begin
        IsolatedStorage.Get("Setup Variable Name", DataScope::Company, retval);
    end;

    var
        storedTxt: Label '---Stored---';
}
