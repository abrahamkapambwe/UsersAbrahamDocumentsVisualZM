﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayGateway.aspx.cs" Inherits="RealEstateWebRole.Members.maps.PayGateway" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function LoadingThePaymentvalues(value) {
            var paymentsValues = value.split(';');

            document.getElementById("hdffirstname").value = paymentsValues[0];
            document.getElementById("hdflastname").value = paymentsValues[1];
            document.getElementById("hdfaddress").value = paymentsValues[2];
            document.getElementById("hdfphone_number").value = paymentsValues[3];
            document.getElementById("hdfpostal_code").value = paymentsValues[4];
            document.getElementById("hdfcity").value = paymentsValues[5];
            document.getElementById("hdfstate").value = paymentsValues[6];
            document.getElementById("hdfcountry").value = paymentsValues[7];
            document.getElementById("hdfamount").value = paymentsValues[8];

            //document.getElementById("hdfcurrency").value = paymentsValues[9];
            document.getElementById("hdfmerchant_fields").value = paymentsValues[9];
            document.getElementById("hdfamount2_description").value = paymentsValues[10];

            document.getElementById("hdfreturn_url").value = paymentsValues[11];
            document.getElementById("hdfcancel_url").value = paymentsValues[12];
        }
        function PopulatePrice(value) {
            var paymentsValues = value.split(';')

            document.getElementById("hdfamount").value = paymentsValues[0];
            document.getElementById("hdfamount2_description").value = paymentsValues[1];
        }
        function Post() {
            document.forms[0].action = "https://www.moneybookers.com/app/payment.pl";
            document.forms[0].target = "_parent";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <div>
            <%-- Merchant information--%>
            <input type="hidden" name="pay_to_email" value="violetmibenge@gmail.com" />
            <input type="hidden" name="return_url_target" value="1" />
            <input type="hidden" name="cancel_url_target" value="1" />
            <input type="hidden" id="hdfcancel_url" name="cancel_url" value="" />
            <input type="hidden" name="dynamic_descriptor" value="Descriptor" />
            <input type="hidden" id="hdfreturn_url" name="return_url" value=" " />
            <input type="hidden" name="language" value="EN" />
            <input type="hidden" name="new_window_redirect" value="0" />
            <input type="hidden" name="confirmation_note" value="ePropertySearch  wishes you pleasure reading your new book!" />
            <input type="hidden" id="hdfmerchant_fields" name="merchant_fields" value="field1" />
            <input type="hidden" name="title" value="Mr" />
            <input type="hidden" id="hdffirstname" name="firstname" value="" />
            <input type="hidden" id="hdflastname" name="lastname" value="" />
            <input type="hidden" id="hdfaddress" name="address" value="" />
            <input type="hidden" id="hdfaddress2" name="address2" value="" />
            <input type="hidden" id="hdfphone_number" name="phone_number" value="" />
            <input type="hidden" id="hdfpostal_code" name="postal_code" value="" />
            <input type="hidden" id="hdfcity" name="city" value="" />
            <input type="hidden" id="hdfstate" name="state" value="" />
            <input type="hidden" id="hdfcountry" name="country" value="" />
            <input type="hidden" id="hdfamount" name="amount" value="" />
            <input type="hidden" id="hdfcurrency" name="currency" value="KES" />
            <input type="hidden" id="hdfamount2_description" name="amount2_description" value="Service Price:" />
            <input type="hidden" id="hdfamount2" name="amount2" value="" />
            <input type="hidden" id="hdfamount3_description" name="amount3_description" value="" />
            <%--  <input type="hidden" name="amount3" value="3.10" />
            <input type="hidden" name="amount4_description" value="VAT (20%):" />
            <input type="hidden" name="amount4" value="6.60" />--%>
            <input type="hidden" name="detail1_description" value="Product ID:" />
            <input type="hidden" id="hdfPropertyid" name="detail1_text" value="" />
            <input type="hidden" name="detail2_description" value="Description:" />
            <input type="hidden" id="hdfdetails_description" name="detail2_text" value="" />
            <%--<input type="hidden" name="detail3_description" value="Seller ID:" />
            <input type="hidden" name="detail3_text" value="123456" />
            <input type="hidden" name="detail4_description" value="Special Conditions:" />
            <input type="hidden" name="detail4_text" value="5-6 days for delivery" />--%>
            <input type="hidden" name="rec_period" value="1" />
            <input type="hidden" name="rec_grace_period" value="1" />
            <input type="hidden" name="rec_cycle" value="day" />
            <input type="hidden" name="ondemand_max_currency" value="KES" />
            <input type="hidden" name="ondemand_note" value="Yes" />
            <input type="hidden" name="pm" value="OBT" />
            <input type="hidden" name="payment_methods" value="VSE,MSC,WLT,NGP," />
            <div class="paymentDetails">
                <div>
                    <h3>
                        Purchase Details</h3>
                </div>
                <div style="width: 100%">
                    <div style="width: 10%; float: left">
                        <div class="payments">
                            <asp:LinkButton runat="server" ID="lkBEdit" Text="Edit" OnClick="Edit_OnClick"></asp:LinkButton></div>
                    </div>
                    <div style="width: 30%; float: left">
                        <div style="clear: both">
                            <div class="payments">
                                <b>Property ID</b></div>
                        </div>
                        <div>
                            <div class="payments">
                                <asp:Label runat="server" ID="lblPropertyID"></asp:Label></div>
                        </div>
                    </div>
                    <div style="width: 30%; float: left">
                        <div style="clear: both">
                            <div class="payments">
                                <b>Merchandise</b></div>
                        </div>
                        <div>
                            <div class="payments">
                                <asp:Label runat="server" ID="lblMerchandise"></asp:Label></div>
                        </div>
                    </div>
                    <div style="width: 30%; float: left">
                        <div style="clear: both">
                            <div class="payments">
                                <b>Amount</b></div>
                        </div>
                        <div>
                            <div class="payments">
                                <asp:Label runat="server" ID="lblAmount"></asp:Label>
                                <asp:DropDownList runat="server" Visible="false" ID="ddlAmountEdit">
                                    <asp:ListItem Text="K 50 000" Value="50 000"></asp:ListItem>
                                    <asp:ListItem Text="K 100 000" Value="100 000"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div style="clear: both">
                <p>
                    By pressing the "Pay now" button, I agree to the terms of use for listing my property
                    on eProperty Search
                </p>
            </div>
            <input type="submit" name="Pay" value="Pay" onclick="Post();" />
        </div>
        <div>
            <table>
                <tr>
                    <td style="padding: 10px">
                        <img src="../../images/skrill-moneybookers-logo_100x59.gif" alt="moneybookers" />
                    </td>
                    <td style="padding: 10px">
                        <img src="../../images/maestro-by-mb.gif" alt="maetro" />
                    </td>
                    <td style="padding: 10px">
                        <img src="../../images/bank-by-mb.gif" alt="bank by" />
                    </td>
                    <td style="padding: 10px">
                        <img src="../../images/visadebit-by-mb.gif" alt="visa debit" />
                    </td>
                    <td style="padding: 10px">
                        <img src="../../images/visaelectron-by-mb.gif" alt="visa electron" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
