 # Import the Flask class from the flask package
from flask import Flask

 # Create a Flask application instance
app = Flask(__name__)

 # Define the root route that returns 'Hello, World!'
@app.route('/')
def hello_world():
    return 'Hello, World!'

 # Define the /error route that intentionally raises an exception
@app.route('/error')
def error():
    raise Exception('Intentional error for Application Insights demo!')

 # Run the app if this script is executed directly
if __name__ == '__main__':
    app.run(host='0.0.0.0', port=8000)
