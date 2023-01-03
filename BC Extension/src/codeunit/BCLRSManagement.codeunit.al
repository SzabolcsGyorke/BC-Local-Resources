codeunit 90102 "BC LRS Management"
{
    trigger OnRun()
    begin

    end;

    internal procedure AddToSystemPrinters()
    var
        BCLRSServiceResource: Record "BC LRS Service Resource";
    begin
        BCLRSServiceResource.SetRange("Resource Type", BCLRSServiceResource."Resource Type"::Printer);
        if Page.RunModal(Page::"BC LRS Printers", BCLRSServiceResource) = Action::LookupOK then;
    end;

}