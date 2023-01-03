page 90107 "BCLRSEntriesAPI"
{
    APIGroup = 'bclrs';
    APIPublisher = 'bclrs';
    APIVersion = 'v1.0';
    ApplicationArea = All;
    Caption = 'bclrsEntriesAPI';
    DelayedInsert = true;
    EntityName = 'bclrsEntry';
    EntitySetName = 'bclrsEntries';
    PageType = API;
    SourceTable = "BC LRS Entry";

    layout
    {
        area(content)
        {
            repeater(General)
            {
                field(entryGUID; Rec."Entry GUID")
                {
                    Caption = 'Entry GUID';
                }
                field(entryBlob; Rec."Entry Blob")
                {
                    Caption = 'Entry Blob';
                }
                field(companyName; Rec."Company Name")
                {
                    Caption = 'Company Name';
                }
                field(documentName; Rec."Document Name")
                {
                    Caption = 'Document Name';
                }
                field(entryCompleted; Rec."Entry Completed")
                {
                    Caption = 'Entry Completed';
                }
                field(entryCompletedAt; Rec."Entry Completed At")
                {
                    Caption = 'Entry Completed At';
                }
                field(entryType; Rec."Entry Type")
                {
                    Caption = 'Entry Type';
                }
                field(localResourceCode; Rec."Local Resource Code")
                {
                    Caption = 'Local Resource Code';
                }
                field(localServiceCode; Rec."Local Service Code")
                {
                    Caption = 'Local Service Code';
                }
                field(printCopies; Rec."Print Copies")
                {
                    Caption = 'Print Copies';
                }
                field(useRawPrint; Rec."Use Raw Print")
                {
                    Caption = 'Use Raw Print';
                }
                field(systemCreatedAt; Rec.SystemCreatedAt)
                {
                    Caption = 'SystemCreatedAt';
                }
                field(systemCreatedBy; Rec.SystemCreatedBy)
                {
                    Caption = 'SystemCreatedBy';
                }
                field(systemId; Rec.SystemId)
                {
                    Caption = 'SystemId';
                }
                field(systemModifiedAt; Rec.SystemModifiedAt)
                {
                    Caption = 'SystemModifiedAt';
                }
                field(systemModifiedBy; Rec.SystemModifiedBy)
                {
                    Caption = 'SystemModifiedBy';
                }
            }
        }
    }
}
