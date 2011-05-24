#!/usr/bin/node

var mongoose = require('mongoose'),
    Schema = mongoose.Schema;
mongoose.connect('mongodb://127.0.0.1/test');

var ObjectId = Schema.ObjectId;

var Comments = new Schema({
    title: String,
    body: String,
    date: Date
});

var BlogPost = new Schema({
    author: ObjectId,
    title: String,
    body: String,
    date: Date,
    comments: [Comments],
    meta: {
        votes: Number,
        favs: Number
    }
});

mongoose.model('BlogPost', BlogPost);

// retrieve my model
var BlogPost = mongoose.model('BlogPost');

// create a blog post
var post = new BlogPost();

// create a comment
post.comments.push({ title: 'My comment' });

post.save(function (err) {
  if (!err) console.log('Success!');
});
console.log('...end...');