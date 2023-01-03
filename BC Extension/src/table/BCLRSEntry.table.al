table 90100 "BC LRS Entry"
{
    DataClassification = CustomerContent;
    DataPerCompany = false;
    LookupPageId = 90100;
    DrillDownPageId = 90100;
    fields
    {
        field(1; "Entry GUID"; Guid)
        {
            Caption = 'Entry GUID';
            DataClassification = CustomerContent;
        }
        field(2; "Company Name"; Text[80])
        {
            Caption = 'Company Name';
            DataClassification = CustomerContent;
        }
        field(3; "Entry Type"; Enum "BC LRS Entry Type")
        {
            Caption = 'Entry Type';
            DataClassification = CustomerContent;
        }

        field(4; "Document Name"; Text[250])
        {
            Caption = 'Document Name';
            DataClassification = CustomerContent;
        }

        field(5; "Entry Blob"; Blob)
        {
            Caption = 'Entry Blob';
            DataClassification = CustomerContent;
            Subtype = UserDefined;
        }

        field(6; "Local Service Code"; Text[50])
        {
            DataClassification = CustomerContent;
            Caption = 'Local Service Code';
            TableRelation = "BC LRS Service";
        }

        field(7; "Local Resource Code"; Text[250])
        {
            Caption = 'Local Resource Code';
            DataClassification = CustomerContent;
            TableRelation = "BC LRS Service Resource"."Local Resource Code" where("Local Service Code" = field("Local Service Code"));
            ValidateTableRelation = false;
        }
        field(8; "Print Copies"; Integer)
        {
            Caption = 'Print Copies';
            DataClassification = CustomerContent;
            InitValue = 1;
        }
        field(9; "Entry Completed"; Boolean)
        {
            Caption = 'Entry Completed';
            DataClassification = CustomerContent;
            trigger OnValidate()
            begin
                "Entry Completed At" := CurrentDateTime;
            end;
        }

        field(10; "Entry Completed At"; DateTime)
        {
            DataClassification = CustomerContent;
            Caption = 'Entry Completed At';
        }

        field(11; "Use Raw Print"; Boolean)
        {
            Caption = 'Use Raw Print';
            DataClassification = CustomerContent;
        }


    }

    keys
    {
        key(PK; "Entry GUID")
        {
            Clustered = true;
        }
    }
    trigger OnInsert()
    begin
        "Entry GUID" := CreateGuid();
        "Company Name" := CompanyName();
    end;

}