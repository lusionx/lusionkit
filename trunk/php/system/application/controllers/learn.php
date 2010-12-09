<?php

class Learn extends Controller {

    function __construct() {
        parent::Controller();
    }

    function url() {
        echo site_url("news/local/123") . '---' . base_url();
    }

    function urlex() {
        echo url_test('dasdsada');
    }

    function arr() {
        $this->load->helper('array');
        $array = array('color' => 'red', 'shape' => 'round', 'size' => '');
        // 返回 "red"
        echo element('color', $array);
    }

    function cfg($param='') {
        echo css_url('a.css');
    }

    function tag($param = '') {
        $out = tag_js(js_url($this, 'jquery.js'));
        $this->load->view('learn', array('out' => $out));
    }

    function arrex($param='') {
        $this->load->helper('array');
        $quotes = array(
            "I find that the harder I work, the more luck I seem to have. - Thomas Jefferson",
            "Don't stay in bed, unless you can make money in bed. - George Burns",
            "We didn't lose the game; we just ran out of time. - Vince Lombardi",
            "If everything seems under control, you're not going fast enough. - Mario Andretti",
            "Reality is merely an illusion, albeit a very persistent one. - Albert Einstein",
            "Chance favors the prepared mind - Louis Pasteur"
        );
        echo first_element($quotes);
    }

    function exhelp($param = '0') {
        $this->load->helper('cookie');
        echo cookie_ex_test($this);
    }

    function lib() {
        //自定义 lib
        $this->load->library('query');
        echo '加载自定义lib' . $this->query->index();
    }

    function db() {
        $this->load->database();
        $q = $this->db->get('worklog');
        $this->load->library('table');
        $data['out'] = $this->table->generate($q);
        $this->load->view('learn', $data);
    }

    function encrypt() {
        $this->load->library('encrypt');
        $key = 'super-secret-key';
        $msg = '2010-12-16 17:25:32 2010-12-17 17:25:32 192.168.1.3 lxing';
        $msg = $this->encrypt->encode($msg, $key);
        echo $msg . '-|-|-|-' . strlen($msg);
    }

    function strlen($str = '') {
        echo strlen($str);
    }

    function join() {
        $cookie = array(
            'name' => 'The Cookie Name',
            'value' => 'The Value',
            'expire' => '86500',
            'domain' => '.some-domain.com',
            'path' => '/',
            'prefix' => 'myprefix_',
        );
        echo join('-', $cookie);
    }

}
