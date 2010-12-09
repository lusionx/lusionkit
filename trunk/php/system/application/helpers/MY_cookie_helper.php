<?php

/*
 * 设置 验证 cookie
 * 
 */

function forms_auth_set($username, $expire_m) {
    $cookie = arrry();
    $cookie = array(
        'name' => 'The Cookie Name',
        'value' => 'The Value',
        'expire' => '86500',
        'domain' => '.some-domain.com',
        'path' => '/',
        'prefix' => 'myprefix_',
    );
    set_cookie($cookie);
}

function _get_encode_str($name) {
    //明文
    $arr = array(
        'ip' => '',
        'time' => 'now',
        'expire' => '默认值',
        'name' => $name
    );
    $str = join('/', $arr);
}

function cookie_ex_test($ci) {
    $ci->load->library('query');
    $ci->load->helper('cookie');
    return '在ex-helper中'.$ci->query->index();
}

?>