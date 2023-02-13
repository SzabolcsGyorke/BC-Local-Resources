/// <summary>
/// Table BC LRS Entry (ID 90100). Store the commands sent to the individual clients
/// </summary>
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

        field(6; "Local Service Code"; Text[250])
        {
            DataClassification = CustomerContent;
            Caption = 'Local Service Code';
            TableRelation = "BC LRS Service";
            trigger OnValidate()
            begin
                SetDefaults();
            end;
        }

        field(7; "Local Resource Code"; Text[250])
        {
            Caption = 'Local Resource Code';
            DataClassification = CustomerContent;
            TableRelation = "BC LRS Service Resource"."Local Resource Code" where("Local Service Code" = field("Local Service Code"));
            ValidateTableRelation = false;
            trigger OnValidate()
            begin
                SetDefaults();
            end;
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

    local procedure SetDefaults()
    var
        BCLRSServiceResource: Record "BC LRS Service Resource";
    begin
        "Entry GUID" := CreateGuid();
        BCLRSServiceResource.SetRange("Local Resource Code", Rec."Local Resource Code");
        BCLRSServiceResource.SetRange("Local Service Code", Rec."Local Service Code");
        if BCLRSServiceResource.FindFirst() then begin
            Rec."Company Name" := CopyStr(CompanyName(), 1, MaxStrLen(Rec."Company Name"));
            case BCLRSServiceResource."Resource Type" of
                BCLRSServiceResource."Resource Type"::Printer:
                    Rec."Entry Type" := Rec."Entry Type"::Print;
                BCLRSServiceResource."Resource Type"::"File Queue From Client":
                    Rec."Entry Type" := Rec."Entry Type"::"File From Client";
                BCLRSServiceResource."Resource Type"::"File Queue To Client":
                    Rec."Entry Type" := Rec."Entry Type"::"File To Client";
                BCLRSServiceResource."Resource Type"::"Command to Client":
                    Rec."Entry Type" := Rec."Entry Type"::"Command to Client";
            end;
        end;
    end;

    internal procedure GetCommand() Command: Text
    var
        TypeHelper: Codeunit "Type Helper";
        InStream: InStream;
    begin
        if "Entry Type" <> "Entry Type"::"Command to Client" then exit;

        CalcFields("Entry Blob");
        "Entry Blob".CreateInStream(InStream, TEXTENCODING::UTF8);
        exit(TypeHelper.TryReadAsTextWithSepAndFieldErrMsg(InStream, TypeHelper.LFSeparator(), FieldName("Entry Blob")));
    end;

    procedure SetCommand(NewCommand: Text)
    var
        OutStream: OutStream;
    begin
        if "Entry Type" <> "Entry Type"::"Command to Client" then exit;

        Clear("Entry Blob");
        "Entry Blob".CreateOutStream(OutStream, TEXTENCODING::UTF8);
        OutStream.WriteText(NewCommand);
        if not Modify() then
            Insert(true);
    end;


}