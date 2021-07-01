var postfixList = ["sina.com", "163.com", "qq.com", "126.com", "vip.sina.com", "sina.cn", "hotmail.com", "gmail.com", "souhu.com", "139.com", "wo.com.cn", "189.cn", "21.cn.com"];

var sug = document.getElementById("email-sug-wrapper");
//点击事件响应
$(document).click(function (e) {
    var a = $('.emailCheckclass'); //设置空白以外的目标区域
    if (!a.is(e.target) && a.has(e.target).length === 0) {
        hide();
    }
});
sug.addEventListener("click", function (ev) {
    //采用事件代理，监听父级点击事件，通过target获取当前li
    var ev = ev || window.event;
    var target = ev.target || ev.srcElement;
    if (target.nodeName.toLowerCase() == "li") {
        hide();
        txt.focus(); //写在return之前，不然无效
        return txt.value = htmlDecode(target.innerHTML); //解码
        //return txt.value= target.innerHTML;      

    }

})
//键盘事件响应
document.addEventListener("keydown", function (e) {
    var e = e || window.event;
    var key = e.which || e.keyCode;
    var list = document.getElementsByTagName("li");
    //向下键
    if (key == 40) {
        for (i = 0; i < list.length; i++) {
            list[i].setAttribute("class", "");

        }
        nowSelectTipIndex++;
        if (nowSelectTipIndex + 1 > list.length) {
            nowSelectTipIndex = 0;

        }
        list[nowSelectTipIndex].setAttribute("class", "active");

    }
    //向上键
    if (key == 38) {
        for (i = 0; i < list.length; i++) {
            list[i].setAttribute("class", "");

        }
        nowSelectTipIndex--;
        if (nowSelectTipIndex < 0) {
            nowSelectTipIndex = list.length - 1;

        }
        list[nowSelectTipIndex].setAttribute("class", "active");

    }
    //回车键
    if (key == 13) {
        var x = document.getElementsByClassName("active");
        txt.value = htmlDecode(x[0].innerHTML); //用textcontent会去除html标签例如<b>。。
        hide();

    }
    if (key == 27) {
        txt.setSelectionRange(0, -1); //ESC全选上文本框内容
        hide();

    }


})
//获取输入内容，并去除首尾空格
function getText() {
    var inputText = txt.value.trim();
    return inputText;

}
//判断是否生成新的数组
function postlist() {
    var userinput = getText();
    var newpostlist = new Array();
    if (userinput.search('@') != 0) {
        var len = userinput.search('@');
        //用来拼接的用户输入内容 = 只使用@之后的字符串
        var x = userinput.substring(len + 1, userinput.length); //取@之后的部分
        for (var i = 0; i < postfixList.length; i++) {
            if (postfixList[i].search(x) == 0) {
                newpostlist.push(postfixList[i]);

            }

        }
        //若@后面没有字符或者新数组newpostlist为空，就返回原来的postfixlist
        if (x === '' || newpostlist == '') {
            return postfixList;

        }
        return newpostlist;

    } else {
        return postfixList;

    }

}
//根据输入内容和匹配来生成提示数组
function promptContent() {
    var x = htmlEncode(getText()); //转码;
    // var x=getText();
    var tips = new Array();
    if (x.indexOf("@") != -1) {
        var p = x.slice(0, x.indexOf("@"));
        for (i = 0; i < postlist().length; i++) {
            tips[i] = p + "@" + postlist()[i];

        }

    } else {
        for (i = 0; i < postfixList.length; i++) {
            tips[i] = x + "@" + postfixList[i];

        }

    }
    return tips;

}
//添加提示数组进入li       
function add() {
    var sug = document.getElementById("email-sug-wrapper");
    var tips = promptContent();
    while (sug.hasChildNodes()) {
        sug.removeChild(sug.firstChild);

    }
    //将之前的列表清除掉，然后重新生成新的列表
    for (i = 0; i < tips.length; i++) {
        var tip_li = document.createElement("li");
        tip_li.innerHTML = tips[i];
        sug.appendChild(tip_li);

    }
    //初始选择第一项为选中状态，加类名变粉色（需要生成li之后再调用）。
    var list = document.getElementsByTagName("li");
    list[0].setAttribute("class", "active");

}

function judge() {
    //判空，是“”没有内容，不能为“　”
    if (getText() == "") {
        hide();

    } else {
        display();

    }
}
//控制提示列表隐藏
function hide() {
    sug.style.display = "none";

}
//控制提示列表显示  
function display() {
    sug.style.display = "block";

}

/*1.用浏览器内部转换器实现html转码*/
function htmlEncode(html) {
    //1.首先动态创建一个容器标签元素，如DIV
    var temp = document.createElement("div");
    //2.然后将要转换的字符串设置为这个元素的innerText(ie支持)或者textContent(火狐，google支持)
    (temp.textContent != undefined) ? (temp.textContent = html) : (temp.innerText = html);
    //3.最后返回这个元素的innerHTML，即得到经过HTML编码转换的字符串了
    var output = temp.innerHTML;
    temp = null;
    return output;

}
/*2.用浏览器内部转换器实现html解码*/
function htmlDecode(text) {
    //1.首先动态创建一个容器标签元素，如DIV
    var temp = document.createElement("div");
    //2.然后将要转换的字符串设置为这个元素的innerHTML(ie，火狐，google都支持)
    temp.innerHTML = text;
    //3.最后返回这个元素的innerText(ie支持)或者textContent(火狐，google支持)，即得到经过HTML解码的字符串了。
    var output = temp.innerText || temp.textContent;
    temp = null;
    return output;

}