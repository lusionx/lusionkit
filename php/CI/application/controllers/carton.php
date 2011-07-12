<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class Carton extends CI_Controller {
    function __construct()
    {
        parent::__construct();

    }

    public function index()
    {
        #$this->load->library('table');
        $this->load->database();
        $this->db->
            from('carton')->
            #where('name','1')->
            order_by('name','desc')->
            select('name,last');
        $data['query'] = $this->db->get()->result();
        $data['title'] = '新番列表';
        $data['thead'] = array('名称','最终');
        $data['content'] = $this->load->view('carton_list',$data,true);
        $this->load->view('master',$data);
    }
    
    public function add()
    {
        $this->load->helper('form');
        $data['title'] = '新番添加';
        $data['content'] = $this->load->view('carton_add',$data,true);
        $this->load->view('master',$data);
    }
}
