<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class User extends CI_Controller {
    function __construct()
    {
        parent::__construct();
    }

    public function index()
    {
        $data['title'] = '页面标题';
        $data['content'] = $this->load->view('user/list',$data,true);
        $this->load->view('master',$data);
    }
}
