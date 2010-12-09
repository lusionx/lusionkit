/// <reference name="MicrosoftAjax.js"/>
/*
* Script from NETTUTS.com [by James Padolsey]
* @requires jQuery($), jQuery UI & sortable/draggable UI modules
*/
var PPage = function() {
    this.Column0 = new Array();
    this.Column1 = new Array();
    this.Column2 = new Array();
    this.Columns = function(i) {
        var arr = [this.Column0, this.Column1, this.Column2];
        return arr[i];
    }
}
var WidgetData = function() {
    this.Color = '';
    this.ControlName = '';
}
var iNettuts = {

    jQuery: $,

    settings: {
        columns: '.column',
        widgetSelector: '.widget',
        handleSelector: '.widget-head',
        contentSelector: '.widget-content',
        widgetDefault: {
            movable: true,
            removable: true,
            collapsible: true,
            editable: true,
            colorClasses: ['color-yellow', 'color-red', 'color-blue', 'color-white', 'color-orange', 'color-green']
        },
        widgetIndividual: {
            intro: {
                movable: false,
                removable: false,
                collapsible: false,
                editable: false
            }
        }
    },

    afterChange: function() {
        var $ = this.jQuery;
        var pp = new PPage();
        var widget = null;
        $(this.settings.columns).each(function(i) {
            $('li[ctr]', this).each(function() {
                widget = new WidgetData();
                widget.Color = $(this).attr('class');
                widget.ControlName = $(this).attr('ctr');
                pp.Columns(i).push(widget);
            });
        });
        var json = JSON.stringify(pp);
        $.post(appPath + 'Handlers/SavePage.ashx', {
            json: json
        },
        function(result) {
            if (result == 0) {
                window.location.href = window.location.href;
            } else { }
        });
    },

    init: function() {
        //this.attachStylesheet('inettuts.js.css');         
        this.addWidgetControls();
        this.makeSortable();
    },

    getWidgetSettings: function(id) {
        var $ = this.jQuery,
        settings = this.settings;
        return (id && settings.widgetIndividual[id]) ? $.extend({},
        settings.widgetDefault, settings.widgetIndividual[id]) : settings.widgetDefault;
    },

    addWidgetControls: function() {
        var iNettuts = this,
        $ = this.jQuery,
        settings = this.settings;

        $(settings.widgetSelector, $(settings.columns)).each(function() {
            var thisWidgetSettings = iNettuts.getWidgetSettings(this.id);
            if (thisWidgetSettings.removable) {
                $('<a href="#" class="remove">CLOSE</a>').mousedown(function(e) {
                    e.stopPropagation();
                }).click(function() {
                    if (confirm('This widget will be removed, ok?')) {
                        $(this).parents(settings.widgetSelector).animate({
                            opacity: 0
                        },
                        function() {
                            $(this).wrap('<div/>').parent().slideUp(function() {
                                $(this).remove();
                                iNettuts.afterChange();
                            });
                        });
                    }
                    return false;
                }).appendTo($(settings.handleSelector, this));
            }

            if (thisWidgetSettings.editable) {
                $('<a href="#" class="edit">EDIT</a>').mousedown(function(e) {
                    e.stopPropagation();
                }).toggle(function() {
                    $(this).css({
                        backgroundPosition: '-66px 0',
                        width: '32px'
                    }).parents(settings.widgetSelector).find('.edit-box').slideDown('fast');
                    return false;
                },
                function() {
                    $(this).css({
                        backgroundPosition: '0 0',
                        width: '24px'
                    }).parents(settings.widgetSelector).find('.edit-box').slideUp('fast');
                    return false;
                }).appendTo($(settings.handleSelector, this));
                $('<div class="edit-box" style="display:none;"><ul /></div>').children('ul')
                //.append('<li class="item"><br/><label>Change the title?</label><input value="' + $('h3', this).text().trim() + '"/></li>')
                .append((function() {
                    var colorList = '<li class="item"><label>Available colors:</label><ul class="colors">';
                    $(thisWidgetSettings.colorClasses).each(function() {
                        colorList += '<li class="' + this + '"/>';
                    });
                    return colorList + '</ul>';
                })()).parent().insertAfter($(settings.handleSelector, this));
            }

            if (thisWidgetSettings.collapsible) {
                $('<a href="#" class="collapse">COLLAPSE</a>').mousedown(function(e) {
                    e.stopPropagation();
                }).toggle(function() {
                    $(this).css({
                        backgroundPosition: '-38px 0'
                    }).parents(settings.widgetSelector).find(settings.contentSelector).slideUp('fast');
                    return false;
                },
                function() {
                    $(this).css({
                        backgroundPosition: '-52px 0'
                    }).parents(settings.widgetSelector).find(settings.contentSelector).slideDown('fast');
                    return false;
                }).prependTo($(settings.handleSelector, this));
            }
        });

        $('.edit-box').each(function() {
            /* $('input', this).keyup(function() {
            $(this).parents(settings.widgetSelector).find('h3').text($(this).val().length > 20 ? $(this).val().substr(0, 20) + '...' : $(this).val());
            });*/
            $('ul.colors li', this).click(function() {

                var colorStylePattern = /\bcolor-[\w]{1,}\b/,
                thisWidgetColorClass = $(this).parents(settings.widgetSelector).attr('class').match(colorStylePattern)
                if (thisWidgetColorClass) {
                    $(this).parents(settings.widgetSelector).removeClass(thisWidgetColorClass[0]).addClass($(this).attr('class').match(colorStylePattern)[0]);
                }
                iNettuts.afterChange();
                return false;
            });
        });

    },

    attachStylesheet: function(href) {
        var $ = this.jQuery;
        return $('<link href="' + href + '" rel="stylesheet" type="text/css" />').appendTo('head');
    },

    makeSortable: function() {
        var iNettuts = this,
        $ = this.jQuery,
        settings = this.settings,
        $sortableItems = (function() {
            var notSortable = '';
            $(settings.widgetSelector, $(settings.columns)).each(function(i) {
                if (!iNettuts.getWidgetSettings(this.id).movable) {
                    if (!this.id) {
                        this.id = 'widget-no-id-' + i;
                    }
                    notSortable += '#' + this.id + ',';
                }
            });
            return $('> li:not(' + notSortable + ')', settings.columns);
        })();

        $sortableItems.find(settings.handleSelector).css({
            cursor: 'move'
        }).mousedown(function(e) {
            $sortableItems.css({
                width: ''
            });
            $(this).parent().css({
                width: $(this).parent().width() + 'px'
            });
        }).mouseup(function() {
            if (!$(this).parent().hasClass('dragging')) {
                $(this).parent().css({
                    width: ''
                });
            } else {
                $(settings.columns).sortable('disable');
            }
        });

        $(settings.columns).sortable({
            items: $sortableItems,
            connectWith: $(settings.columns),
            handle: settings.handleSelector,
            placeholder: 'widget-placeholder',
            forcePlaceholderSize: true,
            revert: 300,
            delay: 100,
            opacity: 0.8,
            containment: 'document',
            start: function(e, ui) {
                $(ui.helper).addClass('dragging');
            },
            stop: function(e, ui) {
                $(ui.item).css({
                    width: ''
                }).removeClass('dragging');
                $(settings.columns).sortable('enable');
                iNettuts.afterChange();
            }
        });
    }
};
$(function() {
    iNettuts.init();
})