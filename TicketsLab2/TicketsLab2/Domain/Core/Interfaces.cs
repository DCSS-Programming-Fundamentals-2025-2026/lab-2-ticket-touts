interface IPrintable
{
    string Print();
}

interface IReceiptLine
{
    string ToReceiptLine(string clientInfo);
}
