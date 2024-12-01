document.getElementById('submitBtn').addEventListener('click', function() {
    const name = document.getElementById('name').value;
    const email = document.getElementById('email').value;
    const subject = document.getElementById('subject').value;
    const message = document.getElementById('message').value;
  
    fetch('https://localhost:44319/api/Contact', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        name: name,
        email: email,
        subject: subject,
        message: message,
      }),
    })
    .then(response => response.json())
    .then(data => {
      alert('Message sent successfully!');
      console.log(data);
    })
    .catch((error) => {
      console.error('Error:', error);
    });
  });