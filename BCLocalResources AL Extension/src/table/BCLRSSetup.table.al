table 90103 "BC LRS Setup"
{
    DataClassification = CustomerContent;

    fields
    {
        field(1; "Primary Key"; Code[10])
        {
            DataClassification = CustomerContent;

        }

        field(10; "Heart Beat Keep Alive (min)"; Integer)
        {
            Caption = 'Heart Beat Keep Alive (min)';
            DataClassification = CustomerContent;

        }

        field(20; "Service Resource Nos."; Code[20])
        {
            DataClassification = CustomerContent;
            Caption = 'Service Resource Nos.';
            TableRelation = "No. Series";
        }
    }

    keys
    {
        key(PK; "Primary Key")
        {
            Clustered = true;
        }
    }


}