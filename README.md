LoveSeat: A light-weight editor for CouchDB Views
========================================

LoveSeat started with three simple goals in mind

* get out of UTF8 CR/LF hell
* avoid having to escape quotation marks
* avoid having to manually tweak json docs

Current features
--------------

* Works on Windows, and Linux and MacOS through Mono
* CRUD on couchdb design docs, views, and lucene indices
* Browse database and view structure through a tree representation
* Export all design documents to disk as .js files
* Import all .js files on disk into a database as design docs
* Clone docs in one database to another (deploy from local dev to remote prod)
* Syntax highlighting (windows javascript views only)
* Support for _show and _list 

Coming soon
-------------
* Local view debugging and step-through