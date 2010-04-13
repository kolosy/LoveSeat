/**
 *   Copyright 2010 Alex Pedenko
 *
 *   Licensed under the Apache License, Version 2.0 (the "License");
 *   you may not use this file except in compliance with the License.
 *   You may obtain a copy of the License at
 *
 *       http://www.apache.org/licenses/LICENSE-2.0
 *
 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *   See the License for the specific language governing permissions and
 *   limitations under the License.
 */

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