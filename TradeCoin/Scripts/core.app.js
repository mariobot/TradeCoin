﻿//-----------------------------------------------------------------------------
//SERVICE
//-----------------------------------------------------------------------------
var coreApp;
(function () {
    coreApp = {
        lang: {
            getTextFromArray: function (arr, val) {
                for (var i in arr) {
                    if (arr[i].value == val)
                        return arr[i].caption;
                }
            },
            GetTextFromDefination: function (arr, val) {
                try {
                    for (var i in arr) {
                        if (arr[i].DataValue == val)
                            return arr[i].Name;
                    }
                }
                catch (ex) {
                    if (typeof console != 'undefined')
                        console.log(ex);
                }
                return "";
            },
        },
        cookie: {
            set: function (name, value, exdays) {
                if (typeof exdays == 'undefined') exdays = 7;
                var exdate = new Date();
                exdate.setDate(exdate.getDate() + exdays);

                value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
                document.cookie = name + "=" + value + '; path=/';
            },
            get: function (name) {
                var i, x, y,
                    r = '',
                    ARRcookies = document.cookie.split(";");
                for (i = 0; i < ARRcookies.length; i++) {
                    x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
                    y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
                    x = x.replace(/^\s+|\s+$/g, "");
                    if (x == name) {
                        r = unescape(y);
                    }
                }
                return r;
            }
        },
        html: {
            encode: function (text) {
                var ret = '';
                text = '' + text;
                if (typeof text == "string") {
                    ret = text
                        .replace(/&/g, '&amp;')
                        .replace(/"/g, '&quot;')
                        .replace(/'/g, '&#39;')
                        .replace(/</g, '&lt;')
                        .replace(/>/g, '&gt;');
                }
                return ret;
            },
            decode: function (text) {
                var ret = '';
                text = '' + text;
                if (typeof text == "string") {
                    ret = text
                        .replace(/&amp;/g, '&')
                        .replace(/&quot;/g, '"')
                        .replace(/&#39;/g, '\'')
                        .replace(/&lt;/g, '<')
                        .replace(/&gt;/g, '>')
                        .replace(/\\u0026amp;/g, '&')
                        .replace(/\\u0026quot;/g, '"')
                        .replace(/\\u0026#39;/g, '\'')
                        .replace(/\\u0026lt;/g, '<')
                        .replace(/\\u0026gt;/g, '>');
                }
                return ret;
            }
        },
        xml: {
            getContent: function (xmlString, tagName) {
                tagName = tagName || "string";
                var ret = xmlString;
                if (xmlString) {
                    ret = $(xmlString).find(tagName).text();
                }
                return ret;
            }
        },
        parseDate: function (val, culture) {
            if (typeof culture == 'undefined') {
                culture = 'vi-VN';
            }
            var ret = val;
            if (val) {
                ret = $.parseDate(val, 'd', culture);
            }
            return ret;
        },
        parseFloat: function (val, culture) {
            var ret = val;
            if (val) {
                if (typeof culture == 'undefined') {
                    culture = 'vi-VN';
                }
                ret = $.parseFloat(val, "n", culture);

                if (isNaN(ret)) {
                    ret = 0;
                }
            }

            return ret;
        },

        formatNumber: function (number, culture, displayScale, displayDecimal, zeroOrEmptyDisplayTo) {
            var val = number;
            if (typeof number != 'undefined') {
                if (typeof culture == 'undefined') culture = coreApp.systemConfig.culture;
                if (typeof displayScale == 'undefined') displayScale = 1;
                if (typeof displayDecimal == 'undefined') displayDecimal = 0;

                number = parseFloat(number);

                if (!isNaN(number)) {
                    if (number == 0 && typeof zeroOrEmptyDisplayTo != 'undefined') {
                        val = zeroOrEmptyDisplayTo;
                    }
                    else {
                        val = formatScale(number, displayScale, displayDecimal, culture);
                    }
                }
                else if (typeof zeroOrEmptyDisplayTo != 'undefined') {
                    val = zeroOrEmptyDisplayTo;
                }

                function formatScale(value, displayScale, displayDecimal, culture) {
                    value = value * displayScale;
                    $.preferCulture(culture);

                    var numValue = $.parseFloat(value, "n", 'en-US');
                    return $.format(numValue, "n" + displayDecimal);
                }
            }
            return val;
        },
        formatDate: function (val, format, culture) {
            var ret = val;
            if (val != "") {
                if (typeof culture == 'undefined') culture = coreApp.systemConfig.culture;
                if (typeof format == 'undefined') format = "d";
                if (typeof val == "string") val = coreApp.getDate(val);
                ret = $.format(val, format, culture);
            }
            return ret;
        },
        formatTime: function (val, format, culture) {
            var ret = val;
            if (val != "") {
                if (typeof culture == 'undefined') culture = coreApp.systemConfig.culture;
                if (typeof format == 'undefined') format = "d";
                if (typeof val == "string") val = coreApp.getDate(val);

                ret = $.format(val, 'hh:mm:ss', culture);
            }
            return ret;
        },
        formatDateTime: function (val, format, culture) {
            var ret = val;
            if (val != "") {
                if (typeof culture == 'undefined') culture = coreApp.systemConfig.culture;
                if (typeof format == 'undefined') format = "dd/MM/yyyy HH:mm:ss";
                if (typeof val == "string") val = coreApp.getDate(val);

                ret = $.format(val, format, culture);
            }
            return ret;
        },
        formatEntryTime: function (val) {
            var ret = val;
            if (val) {
                ret = val.split('.')[0];
            }
            return ret;
        },
        callAjax: function (options, callback) {
            if (options) {
                var defOptions = {
                    type: 'POST',
                    dataType: "json",
                    success: function (data, textStatus, jqXHR) {
                        if (data) {
                            if (typeof callback == 'function') {
                                callback(data);
                            }
                        }
                    }
                };

                $.extend(defOptions, options, true);

                return this.ajax(defOptions);
            }
            return null;
        },
        ///npn: Ham goi rieng cho action
        // data: {Success:true, Message:{Name:'OK',Description:'OK'}, Result:1}
        callAjaxAction: function (options, callback) {
            if (options) {
                var defOptions = {
                    type: 'POST',
                    dataType: "xml",
                    success: function (data, textStatus, jqXHR) {
                        if (data) {
                            data = coreApp.xml.getContent(data);
                            data = data.CSV2JSON2();
                            //Data nhan duoc la 1 array 2 chieu
                            //data[0] la ket qua trang thai request
                            //data[1] ket qua ham tra ve, neu data[0] false thi data[1] co the khong co
                            var result = {};
                            if (data == undefined || data.length == 0 || data[0].length == 0) {
                                result.Message = { Name: "Error", Description: "Unknow error: no data received." };
                                result.Success = false;
                            } else if (data[0][0].Result != '' && data[0][0].Result > 0) { //Neu data[0] OK
                                if (data.length > 1 && data[1].length > 0) {
                                    if (data[1][0].Result != '' && data[1][0].Result > 0) { //Neu data[1] OK
                                        result.Message = data[1][0];
                                        result.Result = data[1][0].Result;
                                        result.Success = true;
                                        if (data.length > 2) { //Neu co tra ve du lieu
                                            result.Entry = data[2];
                                        }
                                    } else {
                                        result.Message = data[1][0];
                                        result.Success = false;
                                    }
                                } else {
                                    result.Message = { Name: "Error", Description: "Unknow error: Request ok." };
                                    result.Success = false;
                                }

                            } else {
                                result.Message = data[0][0];
                                result.Success = false;
                            }
                            if (typeof callback == 'function') {
                                callback(result);
                            }
                        }
                    }
                };

                $.extend(defOptions, options, true);

                return this.ajax(defOptions);
            }
            return null;
        },
        objectToXML: function (tagName, data) {
            var objThis = this;
            var inputs = [],
                ret = '';
            angular.forEach(data, function (value, key) {
                if (typeof value != "function" && typeof value != "object") {
                    // value = html.decode(value);
                    inputs.push($.string.Format('{0}="{1}" ', key, objThis.html.encode(value)));
                }
            });
            ret = $.string.Format('<{0} {1}/>', tagName, inputs.join(' '));

            return ret;
        },
        objectToXMLEx: function (tagName, data) {
            var objThis = this;
            var inputs = [],
                ret = '';
            var childInput = "";
            angular.forEach(data, function (value, key) {
                if (key.indexOf("$") >= 0) return;
                if (typeof value != "function" && typeof value != "object") {
                    // value = html.decode(value);
                    inputs.push($.string.Format('{0}="{1}" ', key, objThis.html.encode(value)));
                } else if (angular.isArray(value)) {
                    angular.forEach(value, function (item, notuse) {
                        childInput += objThis.objectToXMLEx(key, item);
                    });
                } else if (angular.isObject(value)) {
                    childInput = objThis.objectToXMLEx(key, value);
                }
            });
            ret = $.string.Format('<{0} {1}>', tagName, inputs.join(' '));
            ret += childInput;
            ret += $.string.Format('</{0}>', tagName);
            return ret;
        },
        ajax: function (options) {
            options.timeout = 3600000;
            options.url = location.origin + '/' + options.url;

            var showOverlay = options.showOverlay,
                beforeSend = options.beforeSend,
                error = options.error,
                success = options.success;

            //custom 
            options.beforeSend = function (jqXHR, settings) {
                if (typeof beforeSend == 'function') {
                    beforeSend(jqXHR, settings);
                }
                if (showOverlay) {
                    //show overlay here
                }
            };
            options.error = function (jqXHR, textStatus, errorThrown) {
                if (typeof error == 'function') {
                    error(jqXHR, textStatus, errorThrown);
                }
                if (showOverlay) {
                    //hide overlay here
                }
            };

            options.success = function (data, textStatus, jqXHR) {
                if (typeof success == 'function') {
                    success(data, textStatus, jqXHR);
                }
                if (showOverlay) {
                    //hide overlay here
                }
            };

            return $.ajax(options);
        },
        toASCi: function (str) {
            /* str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
             str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ.+/g, "e");
             str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
             str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ.+/g, "o");
             str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
             str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
             str = str.replace(/đ/g, "d");
             */
            str = this.decodeString(str);
            str = str.replace(/-+-/g, "-"); //thay thế 2- thành 1-
            str = str.replace(/^\-+|\-+$/g, "");
            return str;
        },
        decodeString: function (slug) {
            //Đổi ký tự có dấu thành không dấu
            slug = slug.replace(/á|à|ả|ạ|ã|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ/gi, "a");
            slug = slug.replace(/é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ/gi, "e");
            slug = slug.replace(/i|í|ì|ỉ|ĩ|ị/gi, "i");
            slug = slug.replace(/ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ/gi, "o");
            slug = slug.replace(/ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự/gi, "u");
            slug = slug.replace(/ý|ỳ|ỷ|ỹ|ỵ/gi, "y");
            slug = slug.replace(/đ/gi, "d");
            //Xóa các ký tự đặt biệt
            slug = slug.replace(/\`|\~|\!|\@|\#|\||\$|\%|\^|\&|\*|\(|\)|\+|\=|\,|\.|\/|\?|\>|\<|\"|\"|\:|\;|_/gi, "");
            //Đổi khoảng trắng thành ký tự gạch ngang
            slug = slug.replace(/ /gi, " - ");
            //Đổi nhiều ký tự gạch ngang liên tiếp thành 1 ký tự gạch ngang
            //Phòng trường hợp người nhập vào quá nhiều ký tự trắng
            slug = slug.replace(/\-\-\-\-\-/gi, "-");
            slug = slug.replace(/\-\-\-\-/gi, "-");
            slug = slug.replace(/\-\-\-/gi, "-");
            slug = slug.replace(/\-\-/gi, "-");
            //Xóa các ký tự gạch ngang ở đầu và cuối
            slug = "@" + slug + "@";
            slug = slug.replace(/\@\-|\-\@|\@/gi, "");
            return slug;
        },
        iFrame: function (obj) {
            try {
                var iFrame = $(obj)[0];
                var iFrameWindow = iFrame.contentWindow ? iFrame.contentWindow : iFrame.contentDocument.defaultView;
                return iFrameWindow;
            }
            catch (ex) {

            }
        },
        GetFunctionFromiFrame: function (frame, methodName, methodParam, count, limit, callback) {
            var thisObj = this;
            count = count || 0;
            limit = limit || 20;
            if (frame && methodName) {
                if (typeof frame == "string") {
                    if (frame.indexOf("#") == -1 && frame.indexOf(".") == -1)
                        frame = "#" + frame;
                }
                var fr = thisObj.iFrame(frame);
                if (typeof fr != "undefined" && fr) {
                    var method = "fr." + methodName;
                    var fn = eval(method);
                    if (fr.document.readyState == "complete" && typeof fn == "function") {
                        var ret = fn(methodParam);
                        if (typeof callback == "function") {
                            callback();
                        }
                        return ret;
                    }
                    count++;
                    if (count >= limit) {
                        if (typeof callback == "function") {
                            callback();
                        }
                        return false;
                    }
                    window.setTimeout(function () {
                        return thisObj.GetFunctionFromiFrame(frame, methodName, methodParam, count, limit, callback);
                    }, 1000);
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        },
        CallFunctionFromiFrame: function (frameID, methodName, methodParam, callback, limit) {
            return this.GetFunctionFromiFrame(frameID, methodName, methodParam, 0, limit, callback);
        },
        callAjaxGateway: function (options) {
            options.timeout = 3600000;
            options.url = location.origin + '/' + options.url;

            var showOverlay = options.showOverlay,
                beforeSend = options.beforeSend,
                error = options.error,
                success = options.callback;
            !showOverlay && (showOverlay = true);
            !options.dataType && (options.dataType = "JSON");
            !options.type && (options.type = "POST");
            //custom 
            options.beforeSend = function (jqXHR, settings) {
                if (typeof beforeSend == 'function') {
                    beforeSend(jqXHR, settings);
                }
                if (showOverlay) {
                    $(".modalContentOverlay").removeClass('hide')
                }
            };
            options.error = function (jqXHR, textStatus, errorThrown) {
                if (typeof error == 'function') {
                    error(jqXHR, textStatus, errorThrown);
                }
                if (showOverlay) {
                    $(".modalContentOverlay").addClass('hide')
                }
            };

            options.success = function (data, textStatus, jqXHR) {
                if (showOverlay) {
                    $(".modalContentOverlay").addClass('hide')
                }
                if (typeof success == 'function') {
                    setTimeout(function () { success(data, textStatus, jqXHR); }, 100);
                   
                }

               
            };

            return $.ajax(options);
        },
        alertConfirm: function (opt) {
            openModal({ typeModal: 5, type: 2, title: opt.title, content: opt.content, callback: opt.callback });
        },
        alertConfirm_Edit_Del: function (opt) {
            openModal({ typeModal: 5, type: 4, title: opt.title, content: opt.content, callback: opt.callback });
        },
        alertWarning: function (opt) {
            openModal({ typeModal: 5, type: 2, title: opt.title, content: opt.content, callback: opt.callback });
        },
        alertInfo: function (opt) {
            openModal({ typeModal: 5, type: 1, title: opt.title, content: opt.content, callback: opt.callback });
        },
        alertError: function (opt) {
            openModal({ typeModal: 2, type: 3, title: opt.title, content: opt.content, callback: opt.callback });
        },

        notificationWarning: function (_title, _message, callBack) {
            var notice = new PNotify({
                title: _title,
                text: _message,                  
                after_close: function () {
                    if (callBack != undefined && callBack != null)
                        callBack();
                },
                buttons: {
                    closer: false,
                    sticker: false
                }
            });
            notice.get().click(function () {
                notice.remove();
            });
        },
        notificationSuccess: function (_title, _message, callBack, position) {
            var title = 'Information';
            if (position > 0) {
                title = _title;
            }
            var notice = new PNotify({
                //title: _title,
                title: title,
                text: _message,
                type: 'success',
                after_close: function () {
                    if (callBack != undefined && callBack != null)
                        callBack();
                },
                buttons: {
                    closer: false,
                    sticker: false
                }
            });
            notice.get().click(function () {
                notice.remove();
            });
        },
        notificationInfo: function (_title, _message, callBack) {
            var notice = new PNotify({
                //title: _title,
                title: 'Information',
                text: _message,
                type: 'info',
                after_close: function () {
                    if (callBack != undefined && callBack != null)
                        callBack();
                },
                buttons: {
                    closer: false,
                    sticker: false
                }
            });
            notice.get().click(function () {
                notice.remove();
            });
        },
        notificationError: function (_title, _message, callBack) {
            var notice = new PNotify({
                 //title: _title,
                title: 'Information',
                text: _message,
                type: 'error',
                after_close: function () {
                    if (callBack != undefined && callBack != null)
                        callBack();
                },
                buttons: {
                    closer: false,
                    sticker: false
                }
            });
            notice.get().click(function () {
                notice.remove();
            });
        },
                              
       
    }
})();
(function (a) {
    a.systemConfig = {
        clientKey: a.cookie.get('iMarket:Session:Username'),
        userName: a.cookie.get('iMarket:Session:Username'),
        userID: a.cookie.get('iMarket:Session:UserID'),
        sessionKey: a.cookie.get('iMarket:Session:SessionID'),
        clientGroupID: a.cookie.get('iMarket:Session:ClientGroupID'),
    };
})(coreApp);
(function (a) {
    a.service = {
        core: function () {
            userID: 0,
            this.execute = function (fnName, inputValue, callback) {
                return a.callAjax({
                    url: 'Core/CoreService.asmx/' + fnName,
                    data: {
                        clientKey: a.systemConfig.clientKey,
                        inputValue: a.html.encode(inputValue)
                    }
                }, callback);
            };
            this.getInitPage = function (pageCode, languageId, callback) {
                return a.callAjax({
                    url: 'ServiceData/InitPage',
                    data: {
                        pageCode: pageCode,
                        languageId: languageId
                    }
                }, callback);
            };

            this.execute2 = function (url, fnName, inputValue, callback) {
                return a.callAjax({
                    url: url + '/' + fnName,
                    data: {
                        clientKey: a.systemConfig.clientKey,
                        inputValue: a.html.encode(inputValue)
                    }
                }, callback);
            };
            this.executeAction = function (fnName, inputValue, callback) {
                return a.callAjaxAction({
                    url: 'Core/CoreService.asmx/' + fnName,
                    data: {
                        clientKey: a.systemConfig.clientKey,
                        ClientKey: a.systemConfig.clientKey,
                        inputValue: a.html.encode(inputValue)
                    }
                }, callback);
            };
            this.getContextData = function (inputValue, callback) {
                return this.execute('GetContextData', inputValue, callback);
            };
            this.getViewData = function (objInput, callback) {
                var inputValue = a.objectToXML('InputValue', { UserID: this.userID }) + a.objectToXML('RequestParams', objInput); //$.string.Format('<InputValue UserID=""/><RequestParams Sys_ViewID="{0}"/>', viewID);
                return this.execute('GetContextData ', inputValue, callback);
            };
            this.getList = function (viewID, callback) {
                var inputValue = a.objectToXML('InputValue', { UserID: this.userID }) + a.objectToXML('RequestParams', { Sys_ViewID: viewID }); //$.string.Format('<InputValue UserID=""/><RequestParams Sys_ViewID="{0}"/>', viewID);
                return this.execute('GetContextData ', inputValue, callback);
            };
            this.getListEx = function (option, callback) {
                var inputValue = a.objectToXML('InputValue', { UserID: this.userID }) + a.objectToXML('RequestParams', option); //$.string.Format('<InputValue UserID=""/><RequestParams Sys_ViewID="{0}"/>', viewID);
                return this.execute('GetContextData ', inputValue, callback);
            };
            this.actionEntry = function (data, callback) {
                var inputValue = a.objectToXMLEx('InputValue', { UserID: this.userID }) + a.objectToXMLEx('RequestParams', data);
                //console.log("inputValue", inputValue);
                return this.execute('ExecuteAction ', inputValue, callback);
            };
            this.actionEntry2 = function (data, callback) {
                var inputValue = a.objectToXMLEx('InputValue', { UserID: this.userID }) + a.objectToXMLEx('RequestParams', data);
                //console.log("inputValue", inputValue);
                return this.executeAction('ExecuteAction ', inputValue, callback);
            };
            this.callServer = function (url, fnName, data, callback) {
                var inputValue = a.objectToXML('InputValue', data);
                return this.execute2(url, fnName, inputValue, callback);
            };
            //unAssignName
            this.toASCi = function (str) {
                return this.toASCi(str);
            },
            //grid server side processing data
            this.convertServerDataProcessing = function (data) {
                //$.string.Format('<InputValue UserID=""/><RequestParams Sys_ViewID="{0}"/>', viewID);
                var inputValue = a.objectToXML('InputValue', { UserID: this.userID }) + a.objectToXML('RequestParams', data);
                return a.html.encode(inputValue);
            }
        }

    }
})(coreApp);
//-----------------------------------------------------------------------------
//SERVICE
//-----------------------------------------------------------------------------
//angular.module('app.service', [])
//    .service('coreService', coreApp.service.core)

//$(function () {
//    $(document).on('click', '[data-dismiss=modal]', function () {
//        $('.modal-backdrop').remove();
//    });
//});


function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}