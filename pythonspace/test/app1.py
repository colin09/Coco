from flask import Flask ,request , render_template

app = Flask(__name__)

@app.route('/',methods=['GET','POST'])
def home():
	return render_template('home.html')

@app.route('/signin',methods=['GET'])
def signin_form():
	return render_template('form.html')

@app.route('/signin',methods=['POST'])
def signin():
	# 需要从request对象读取表单内容：
	name = request.form['username']
	pwd = request.form['password']
	
	if name =='admin' and pwd =='password':
		return render_template('home.html',username=name)
	return render_template('form.html',message='Bad username or password',username=name)
	

if __name__ == '__main__':
    app.run()