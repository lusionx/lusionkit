<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class User extends CI_Controller {
    function __construct()
    {
        parent::__construct();
    }

    public function index()
    {
        //$this->load->view('welcome_message');
        echo '刘兴';
    }
}

/* End of file welcome.php */
/* Location: ./application/controllers/welcome.php */