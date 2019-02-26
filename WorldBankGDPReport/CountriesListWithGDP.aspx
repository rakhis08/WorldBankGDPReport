<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CountriesListWithGDP.aspx.cs" Inherits="WorldBankGDPReport.CountriesListWithGDP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style6 {
            width: 1018px;
            height: 30px;
        }

        .auto-style9 {
            width: 70%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="margin: 0 auto" class="auto-style9">
        <div>
            <table style="align-items: center; text-align: center; background-color: #CDD1C7;">
                <tr style="align-items: center; text-align: center;">
                    <td style="align-items: center; text-align: center;">
                        <table style="align-items: center; text-align: center;">
                            <tr style="align-items: center; text-align: center;">
                                <td style="align-items: center; text-align: center;" class="auto-style6">
                                    <asp:Label ID="LabelHeader" runat="server" Text="List of the top 10 countries by ‘GDP (current US$)’"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 50%; text-align: right; padding-right: 8px;">
                                    <asp:Label ID="lblDDLMsg" runat="server" Text="Please Select Year:"></asp:Label>
                                </td>
                                <td style="width: 50%; text-align: left;">
                                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" Width="20%">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>2010</asp:ListItem>
                                        <asp:ListItem>2011</asp:ListItem>
                                        <asp:ListItem>2012</asp:ListItem>
                                        <asp:ListItem>2013</asp:ListItem>
                                        <asp:ListItem>2014</asp:ListItem>
                                        <asp:ListItem>2015</asp:ListItem>
                                        <asp:ListItem>2016</asp:ListItem>
                                        <asp:ListItem>2017</asp:ListItem>
                                        <asp:ListItem>2018</asp:ListItem>
                                        <asp:ListItem>2019</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="align-items: center; text-align: center;">
                    <td>
                        <asp:GridView ID="gridTopTenList" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" Width="50%" HorizontalAlign="Center">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                                <asp:BoundField DataField="Country" HeaderText="Country">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="GDPValue" HeaderText="GDP (current US$)" DataFormatString="{0:c}">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <SortedAscendingCellStyle BackColor="#F4F4FD" />
                            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                            <SortedDescendingCellStyle BackColor="#D8D8F0" />
                            <SortedDescendingHeaderStyle BackColor="#3E3277" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
