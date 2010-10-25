<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true"
    CodeBehind="FormatJS.aspx.cs" Inherits="SLWeb.Test.FormatJS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript" type="text/javascript">
        jQuery(document).ready(function() {
            //jQuery.post('/GetMainpageXML.aspx', null, showInf);
            $.ajax({
                url: '/GetMainpageXML.aspx',
                type: 'GET',
                dataType: 'xml',
                timeout: 1000,
                error: function() {
                    alert('Error loading XML document');
                },
                success: showInf
            });
        });
        jQuery(document).ready(function() {
            jQuery.post('/GetMainpageXML.aspx', null, showInf);
        })
        function showInf(msg) {
            jQuery('#imgNoticeWaiting').hide();
            jQuery('#imgNewsWaiting').hide();
            var childNotices = d.documentElement.childNodes.item(0).childNodes;
            var strNotices = "";
            for (var i = 0; i < parseInt(childNotices.length); i++) {
                var childNoticeInfos = childNotices.item(i).childNodes;
                strNotices += "<li style='margin-top:-20px'><p style='line-height: 0px'><span style='line-height: 15px;width:360px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;'>";
                //strNotices += String.format("<a href=\"#\" title=\"{0}\" onclick=\"mopen('portal/notice.aspx?InformationID={1}')>{2}</a>", childNoticeInfos.item(1).text, childNoticeInfos.item(0).text, childNoticeInfos.item(1).text);
                //alert(strNotices);
                var tmp = String.format("<a href=\"javascript:\" title=\"{0}\" onclick=\"mopen('portal/notice.aspx?InformationID={1}')\">{2}</a>", childNoticeInfos.item(1).text, childNoticeInfos.item(0).text, childNoticeInfos.item(1).text);
                //alert(tmp);
                strNotices += tmp;
                //strNotices += "<a href='portal/notice.aspx?InformationID=" + childNoticeInfos.item(0).text + "' title='" + childNoticeInfos.item(1).text + "' target='_blank'>" + childNoticeInfos.item(1).text +"</a>";
                strNotices += "</span>&nbsp;&nbsp;<img alt='' src='images/new.gif' style='display:" + childNoticeInfos.item(6).text + "' align='absmiddle'/>";
                strNotices += "<div class='date'>" + childNoticeInfos.item(3).text + "&nbsp;&nbsp;&nbsp;&nbsp;";
                if (childNoticeInfos.item(7).text == "All") {
                    strNotices += childNoticeInfos.item(7).text;
                }
                else {
                    strNotices += "<img alt='" + childNoticeInfos.item(7).text + "' src='images/scope.gif' style='cursor:hand;' align='absmiddle'/>";
                }
                strNotices += "</div>";
                strNotices += "<span style='margin-top:-13px;line-height: 15px;width:360px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;'>";
                strNotices += String.format("<a style=\"margin-top: -50px\" href=\"javascript:\" onclick=\"mopen('portal/notice.aspx?InformationID={2}')\" title=\"{0}\" >{1}</a>", childNoticeInfos.item(9).text, childNoticeInfos.item(9).text, childNoticeInfos.item(0).text);

                //strNotices +=String.format("<a style='margin-top: -50px' href='portal/notice.aspx?InformationID=" + childNoticeInfos.item(0).text + "' title='" + childNoticeInfos.item(9).text + "' target='_blank'>" + childNoticeInfos.item(9).text +"</a>
                strNotices += "</span></p></li>"
                //alert(strNotices);
            }
            document.body.all.ul1.innerHTML = strNotices;
            //新闻开始
            var childNewss = d.documentElement.childNodes.item(1).childNodes;
            var strNewss = "";
            var strNewsImage = "";
            var newsWidth = "360";
            for (var i = 0; i < parseInt(childNewss.length); i++) {
                var childNewsInfos = childNewss.item(i).childNodes;
                if (i == 0) {
                    if (childNewsInfos.item(8).text != "") {
                        //strNewsImage = "<a href='portal/news.aspx?InformationID=" + childNewsInfos.item(0).text + "' target='_blank'><img style='width:110px; border:solid 0px #737d84;' alt='" + childNewsInfos.item(1).text + "' src='" + childNewsInfos.item(8).text + "'/><br/></a>";
                        //strNewsImage = String.format("<a href=\"javascript:\" onclick=\"mopen('portal/news.aspx?InformationID={0}')\"><img style='width:110px; border:solid 0px #737d84;' alt=\"{1}\" src=\"{2}\" /><br/></a>",childNewsInfos.item(0).text,childNewsInfos.item(1).text,childNewsInfos.item(8).text);
                        strNewsImage = String.format("<a href=\"javascript:\" onclick=\"mopen('{0}')\"><img style='width:110px; border:solid 0px #737d84;' alt=\"{1}\" src=\"{2}\" /><br/></a>", childNewsInfos.item(8).text.replace('t_', ''), '<%= WisWorkflow.Utility.ModelResourceManager.Current.GetString("点击查看原始图片尺寸") %>', childNewsInfos.item(8).text);
                        strNewsImage += "<br/>" + '<span style="font-size:14px;">&nbsp;&nbsp;&nbsp;' + childNewsInfos.item(10).text + '</span>';
                        newsWidth = "215";
                    }
                }
                strNewss += "<li style='margin-top:0px'><span style='line-height: 15px;width:" + newsWidth + "px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;'>";
                //strNewss += String.format("<a href='portal/news.aspx?InformationID=" + childNewsInfos.item(0).text + "' title='" + childNewsInfos.item(1).text + "' target='_blank'>" + childNewsInfos.item(1).text + "</a>";
                strNewss += String.format("<a href=\"javascript:\" title=\"{0}\" onclick=\"mopen('portal/news.aspx?InformationID={1}')\" >{2}</a>", childNewsInfos.item(1).text, childNewsInfos.item(0).text, childNewsInfos.item(1).text);
                strNewss += "</span>&nbsp;&nbsp;<img alt='' src='images/new.gif' style='display:" + childNewsInfos.item(6).text + "' align='absmiddle'/>";
                strNewss += "<div class='date'>" + childNewsInfos.item(3).text + "&nbsp;&nbsp;&nbsp;&nbsp;"
                if (childNewsInfos.item(7).text == "All") {
                    strNewss += childNewsInfos.item(7).text;
                }
                else {
                    strNewss += "<img alt='" + childNewsInfos.item(7).text + "' src='images/scope.gif' style='cursor:hand;' align='absmiddle'/>";
                }
                strNewss += "</div><br/>";
                strNewss += "<span style='margin-top:-13px;line-height: 15px;width:" + newsWidth + "px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;'>";
                //strNewss +=String.format("<a style='margin-top: -50px' href='portal/news.aspx?InformationID=" + childNewsInfos.item(0).text + "' title='" + childNewsInfos.item(9).text + "' target='_blank'>" + childNewsInfos.item(9).text +"</a>
                strNewss += String.format("<a style=\"margin-top: -50px\" href=\"javascript:\" title=\"{0}\" onclick=\"mopen('portal/news.aspx?InformationID={1}')\" >{2}</a>", childNewsInfos.item(9).text, childNewsInfos.item(0).text, childNewsInfos.item(9).text);

                strNewss += "</span></li>";
            }
            document.body.all.ul2.innerHTML = strNewss;
            if (strNewsImage != "") {
                document.body.all.ctl00_cpContent_RightContent1_lblNewsImage.innerHTML = strNewsImage;
            }
        }
    
    </script>

</asp:Content>
