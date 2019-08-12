
#include <iostream>

template <typename T>
class ThreadedTree {
	struct Node {
		T data_;
		Node* left_;
		Node* right_;
		bool leftThread_ = false;
		bool rightThread_ = false;


		Node(const T& data, Node* left = nullptr, Node* right = nullptr) {
			data_ = data;
			left_ = left;
			right_ = right;
		}
	};

	Node* root_;
	bool goLeft;
	bool goRight;
public:

	class const_iterator {
	protected:
		Node* curr_;
		const_iterator(Node* c) { curr_ = c; }
	public:
		friend class ThreadedTree;
		const_iterator() {
			curr_ = nullptr;
		}


		const_iterator operator++(int) {
			const_iterator old = curr_;
			++(*this);

			return old;
		}

		const_iterator operator--(int) {
			const_iterator  old = curr_;
			--(*this);

			return old;

		}
		const_iterator operator++() {

			if (this->curr_->rightThread_ == true) {
				this->curr_ = this->curr_->right_;
				while (this->curr_->leftThread_ == true)
				{
					this->curr_ = this->curr_->left_;
				}
			}
			else {
				if (this->curr_->rightThread_ == false) {
					if (this->curr_->right_ != nullptr)
					{
						this->curr_ = this->curr_->right_;
					}
				}
			}
			return *this;

		}
		const_iterator operator--() {
			if (this->curr_->leftThread_ == true) {
				this->curr_ = this->curr_->left_;
				while (this->curr_->rightThread_ == true)
				{
					this->curr_ = this->curr_->right_;
				}
			}
			else
			{
				if (this->curr_->leftThread_ == false) {
					if (this->curr_->left_ != nullptr) {
						this->curr_ = this->curr_->left_;
					}
				}
			}
			return *this;

		}
		const T& operator*() {
			return curr_->data_;
		}
		bool operator==(const const_iterator& rhs) const {
			return curr_ == rhs.curr_;
		}
		bool operator!=(const const_iterator& rhs) const {
			return curr_ != rhs.curr_;
		}
	};
	class iterator :public const_iterator {
		iterator(Node* c) :const_iterator(c) {}
	public:
		friend class ThreadedTree;
		iterator() :const_iterator() {}
		const T& operator*() {
			return this->curr_->data_;
		}

		
		iterator operator++(int) {
			iterator old = curr_;
			curr_ = curr_->right_;
			return iterator(old);
		}

		
		iterator operator--(int) {
			iterator old = curr_;
			curr_ = curr_->left_;
			return iterator(old);

		}
		iterator operator++() {

			if (this->curr_->rightThread_ == true) {
				this->curr_ = this->curr_->right_;
				while (this->curr_->leftThread_ == true)
				{
					this->curr_ = this->curr_->left_;
				}
			}
			else {
				if (this->curr_->rightThread_ == false) {
					if (this->curr_->right_ != nullptr)
					{
						this->curr_ = this->curr_->right_;
					}
				}
			}
			return *this;

		}
	
		iterator operator--() {

			if (this->curr_->leftThread_ == true) {
				this->curr_ = this->curr_->left_;
				while (this->curr_->rightThread_ == true)
				{
					this->curr_ = this->curr_->right_;
				}
			}
			else
			{
				if (this->curr_->leftThread_ == false) {

					this->curr_ = this->curr_->left_;

				}
			}
			return *this;

		}


	};

	ThreadedTree() {
		root_ = nullptr;
	}


	iterator insert(const T& data) {
		ThreadedTree<T>::iterator it;
		if (root_ == nullptr) {
			root_ = new Node(data);

		}
		else
		{
			Node* temp = root_;
			while (true)
			{
				if (data < temp->data_) {
					if (temp->leftThread_ == false) {
						goLeft = true;
						goRight = false;
						break;
					}
					else {
						temp = temp->left_;
					}

				}
				else
				{
					if (temp->rightThread_ == false) {
						goLeft = false;
						goRight = true;
						break;
					}
					else {
						temp = temp->right_;
					}
				}

			}


			if (goLeft == true)
			{
				Node* n = new Node(data);

				if (temp->left_ == nullptr) {
					temp->left_ = n;
					n->right_ = temp;
				}
				else {
					n->left_ = temp->left_;
					n->right_ = temp;
					temp->left_ = n;
				}

				n->leftThread_ = temp->leftThread_;
				temp->leftThread_ = true;
				it.curr_ = n;
				return it;

			}

			if (goRight == true) //if we go right
			{
				Node* n = new Node(data);
				if (temp->right_ == nullptr) {
					temp->right_ = n;
					n->left_ = temp;
				}
				else {
					n->right_ = temp->right_;
					n->left_ = temp;
					temp->right_ = n;
				}
				n->rightThread_ = temp->rightThread_;
				temp->rightThread_ = true;

				it.curr_ = n;
				return it;
			}
		}

	}

	iterator find(const T& data) const {
		Node* r = root_->left_;

		if (r->data_ < data) {
			if (r->rightThread_)
				return false;
			r = r->right_;
		}
		else if (r->data_ > data) {
			if (r->leftThread_)
				return iterator(r);

			r = r->left_;
		}
		else
			return iterator(r);
	}

	iterator begin() {
		Node* temp = root_;
		if (temp == nullptr)
		{
			return iterator(temp);
		}
		else
		{
			while (temp->left_ != nullptr) {
				temp = temp->left_;
			}
			return  iterator(temp);
		}
	}


	iterator end() {
		Node* temp = root_;
		if (temp == nullptr)
		{
			return iterator(temp);
		}
		else
		{
			while (temp->right_ != nullptr) {
				temp = temp->right_;
			}
			return iterator(temp);
		}
	}
	const_iterator begin()const {
		Node* temp = root_;
		if (temp == nullptr)
		{
			return const_iterator(temp);
		}
		else
		{
			while (temp->left_ != nullptr) {
				temp = temp->left_;
			}
			return  const_iterator(temp);
		}
	}
	const_iterator end() const {
		Node* temp = root_;
		if (temp == nullptr)
		{
			return const_iterator(temp);
		}
		else
		{
			while (temp->right_ != nullptr) {
				temp = temp->right_;
			}
			return const_iterator(temp);
		}
	}
	~ThreadedTree() {
		root_ = nullptr;
	}

};
