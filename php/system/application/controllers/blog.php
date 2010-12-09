<?php
class Blog extends Controller {
    function __construct() { 
        parent::Controller();
    }
    function demo() {
        echo '你好，世界！&lt;  &gt;';
    }
    function index() {
        $data['todo_list'] = array('Clean House', 'Call Mom', 'Run Errands');
        $data['title'] = "My Real Title";
        $data['heading'] = "My Real Heading";
        $this->load->view('blog/view', $data);
    }
    function table() {
        $this->load->library('table');
        $this->table->set_heading(array('Name', 'Color', 'Size'));
        $this->table->add_row(array('Fred', 'Blue', 'Small'));
        $this->table->add_row(array('Mary', 'Red', 'Large'));
        $this->table->add_row(array('John', 'Green', 'Medium'));
        echo $this->table->generate();
    }
}

