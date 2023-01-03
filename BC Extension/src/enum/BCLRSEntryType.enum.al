enum 90100 "BC LRS Entry Type"
{
    Extensible = true;

    value(0; Print)
    {
        Caption = 'Print';
    }
    value(1; "File To Client")
    {
        Caption = 'File To Client';
    }
    value(2; "File From Client")
    {
        Caption = 'File From Client';
    }
    value(3; "Command to Client")
    {
        Caption = 'Command to Client';
    }
}
