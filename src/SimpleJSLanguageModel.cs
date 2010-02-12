using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoveSeat
{
    public static class SimpleJSLanguageModel
    {
        public static string[] Keywords = new[] 
        {
            "break", "continue","do","for","import","new","this","void","case","default","else",
            "function","in","return","typeof","while","comment","delete","export","if","label",
            "switch","var","with","abstract","implements","protected","boolean","instanceOf","public",
            "byte","int","short","char","interface","static","double","long","synchronized","false",
            "native","throws","final","null","transient","float","package","true","goto","private",
            "catch","enum","throw","class","extends","try","const","finally	","debugger","super"
        };

        public static string[] ReservedWords = new[] 
        {
            "alert","eval","Link","outerHeight","scrollTo","Anchor","FileUpload","location","outerWidth",
            "Select","Area","find","Location","Packages","self","arguments","focus","locationbar","pageXoffset",
            "setInterval","Array","Form","Math","pageYoffset","setTimeout","assign","Frame","menubar","parent",
            "status","blur","frames","MimeType","parseFloat","statusbar","Boolean","Function","moveBy","parseInt",
            "stop","Button","getClass","moveTo","Password","String","callee","Hidden","name","personalbar","Submit",
            "caller","history","NaN","Plugin","sun","captureEvents","History","navigate","print","taint","Checkbox",
            "home","navigator","prompt","Text","clearInterval","Image","Navigator","prototype","Textarea","clearTimeout",
            "Infinity","netscape","Radio","toolbar","close","innerHeight","Number","ref","top","closed","innerWidth",
            "Object","RegExp","toString","confirm","isFinite","onBlur","releaseEvents","unescape","constructor","isNan",
            "onError","Reset","untaint","Date","java","onFocus","resizeBy","unwatch","defaultStatus","JavaArray","onLoad",
            "resizeTo","valueOf","document","JavaClass","onUnload","routeEvent","watch","Document","JavaObject","open",
            "scroll","window","Element","JavaPackage","opener","scrollbars","Window","escape","length","Option","scrollBy"
        };
    }
}