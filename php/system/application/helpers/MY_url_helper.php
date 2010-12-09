<?php

/**
 * Description of MY_url_helper
 *
 * @author Administrator
 */
/*
 * 返回 application 目录
 */
function _static_url() {
    $f = str_replace('\\', '/', FCPATH);
    $p = str_replace('\\', '/', APPPATH);
    return base_url() . str_replace($f, '', $p);
}

function _get_config_item($name) {
    $control = & get_instance();
    return $control->config->item($name);
}

function css_url($path) {
    $con = _get_config_item('content_css');
    return _static_url() . $con . $path;
}

function js_url($path) {
    $con = _get_config_item('content_js');
    return _static_url() . $con . $path;
}

?>
