<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true"
    CodeBehind="WebForm1.aspx.cs" Inherits="SLWeb.Test.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">

        $(function() {
        })

        function ff1() {
            var oo = {};
            var str = Sys.Serialization.JavaScriptSerializer.serialize(oo);
            $.postJSON(str, "WebService1.asmx/HelloWorld", success);
        }

        function ff2() {
            var oo = {};
            oo.s1 = "我";
            var str = Sys.Serialization.JavaScriptSerializer.serialize(oo);
            $.postJSON(str, "WebService1.asmx/HelloWorld2", success);
        }

        function ff3() {
            var oo = {};
            oo.s1 = "我";
            oo.s2 = "你";
            var str = Sys.Serialization.JavaScriptSerializer.serialize(oo);
            $.postJSON(str, "WebService1.asmx/HelloWorld22", success);
        }

        function ff4() {
            var oo = {};
            oo.pp = {};
            oo.pp.Name1 = "nnn1";
            oo.pp.Name2 = "nnn2";
            oo.pp.Age = 3;
            var pares = Sys.Serialization.JavaScriptSerializer.serialize(oo);
            $.postJSON(pares, "WebService1.asmx/HelloWorld3", success);
        }

        function ff5() {
            var oo = {};
            var arr = new Array();
            Array.add(arr, 1);
            Array.add(arr, 2);
            Array.add(arr, 3);
            Array.add(arr, 4);
            oo.ints = arr;
            var pares = Sys.Serialization.JavaScriptSerializer.serialize(oo);
            $.postJSON(pares, "WebService1.asmx/HelloWorld33", success);
        }

        function success(msg) {
            alert(msg.d);
        }

        function success11(msg) {
            var oo = msg.d;
            for (key in oo) {
                if (key != '__type') {
                    $("#inf").append(oo[key]);
                }
            }
        }

        function success12(msg) {
            $.each(msg.d, function(index) {
                $("#inf").append('  ' + index + '  ');
                for (key in this) {
                    if (key != '__type') {
                        $("#inf").append(this[key]);
                    }
                }
            })
        }

        function ff11() {
            var oo = {};
            var str = Sys.Serialization.JavaScriptSerializer.serialize(oo);
            $.postJSON(str, "WebService1.asmx/GetP", success11);
        }

        function ff12() {
            var oo = {};
            var str = Sys.Serialization.JavaScriptSerializer.serialize(oo);
            $.postJSON(str, "WebService1.asmx/GetPs", success12);
        }
    
    </script>

    参数复杂度
    <button type="button" value="" onclick="ff1();">
        简单调用</button>
    <button type="button" value="" onclick="ff2();">
        参数调用</button>
    <button type="button" value="" onclick="ff3();">
        多参数调用</button>
    <button type="button" value="" onclick="ff4();">
        复杂参数</button>
    <button type="button" value="" onclick="ff5();">
        数组参数</button>
    <br />
    返回值复杂度
    <button type="button" value="" onclick="ff11();">
        返回对象</button>
    <button type="button" value="" onclick="ff12();">
        返回数组</button>
    <div id="inf">
    </div>
</asp:Content>
