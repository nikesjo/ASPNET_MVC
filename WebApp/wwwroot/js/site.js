const toggleMenu = () => {
    document.getElementById('menu').classList.toggle('hide');
    document.getElementById('account-buttons').classList.toggle('hide');
}

const checkScreenSize = () => {
    if (window.innerWidth >= 1200) {
        document.getElementById('menu').classList.remove('hide');
        document.getElementById('account-buttons').classList.remove('hide');
    } else {
        if (!document.getElementById('menu').classList.contains('hide')) {
            document.getElementById('menu').classList.add('hide');
        }
        if (!document.getElementById('account-buttons').classList.contains('hide')) {
            document.getElementById('account-buttons').classList.add('hide');
        }
    }
};

window.addEventListener('resize', checkScreenSize);
checkScreenSize();


document.addEventListener('DOMContentLoaded', function () {
    let sw = document.querySelector('#switch-mode')

    sw.addEventListener('change', function () {
        let theme = this.checked ? "dark" : "light"

        fetch(`/sitesettings/changetheme?mode=${theme}`)
            .then(res => {
                if (res.ok)
                    window.location.reload()
                else
                    console.log('response is not ok')
            })
    })
})

document.addEventListener('DOMContentLoaded', function () {
    select()
    searchQuery()
})


function select() {
    try {
        let select = document.querySelector('.select')
        let selected = select.querySelector('.selected')
        let selectOptions = select.querySelector('.select-options')

        selected.addEventListener('click', function () {
            selectOptions.style.display = (selectOptions.style.display === 'block') ? 'none' : 'block'
        })

        let options = selectOptions.querySelectorAll('.option')
        options.forEach(function (option) {
            option.addEventListener('click', function () {
                selected.innerHTML = this.textContent
                selectOptions.style.display = 'none'
                let category = this.getAttribute('data-value')
                selected.setAttribute('data-value', category)
                updateCoursesByFilters()
            })
        })
    }
    catch { }
}

function searchQuery() {
    try {
        document.querySelector('#searchQuery').addEventListener('keyup', function () {
            updateCoursesByFilters()
        })
    }
    catch { }
}

function updateCoursesByFilters() {
    try {
        const category = document.querySelector('.select .selected').getAttribute('data-value') || 'all'
        const searchQuery = document.querySelector('#searchQuery').value

        const url = `/courses?category=${encodeURIComponent(category)}&searchQuery=${encodeURIComponent(searchQuery)}`

        fetch(url)
            .then(res => res.text())
            .then(data => {
                const parser = new DOMParser()
                const dom = parser.parseFromString(data, 'text/html')
                document.querySelector('.items').innerHTML = dom.querySelector('.items').innerHTML

                const pagination = dom.querySelector('.pagination') ? dom.querySelector('.pagination').innerHTML : ''
                document.querySelector('.pagination').innerHTML = pagination
            })
    }
    catch { }
}

document.addEventListener('DOMContentLoaded', function () {
    handleProfileImageUpload()
})

function handleProfileImageUpload() {
    try {
        let fileUploader = document.querySelector('#fileUploader')
        if (fileUploader != undefined) {
            fileUploader.addEventListener('change', function () {
                if (this.files.length > 0) {
                    this.form.submit()
                }
            })
        }
    }
    catch { }
}

const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))

//document.addEventListener('DOMContentLoaded', function () {
//    document.addEventListener('click', function (e) {
//        if (e.target && e.target.matches("a.button-top, a.button-top i")) {
//            e.preventDefault();

//            var link = e.target.closest("a.button-top");
//            var courseId = parseInt(link.getAttribute('data-courseid'), 10);
//            console.log("Course ID:", courseId);

//            // Retrieve token from session or local storage
//            var token = sessionStorage.getItem('token'); // or localStorage.getItem('token');
//            if (!token) {
//                console.log("Token not found");
//                return;
//            }

//            fetch('/courses/savecourse', {
//                method: 'POST',
//                headers: {
//                    'Content-Type': 'application/json',
//                    'Authorization': 'Bearer ' + token // Add bearer token
//                },
//                body: JSON.stringify({ CourseId: courseId })
//            })
//                .then(response => response.json())
//                .then(data => {
//                    if (data.success) {
//                        link.classList.toggle('saved');
//                    } else {
//                        console.log("Error saving course.");
//                    }
//                })
//                .catch(error => {
//                    console.log("Error saving course:", error);
//                });
//        }
//    });
//});

//document.addEventListener('DOMContentLoaded', function () {
//    document.addEventListener('click', function (e) {
//        if (e.target && e.target.matches("a.bookmark, a.bookmark i")) {
//            e.preventDefault();

//            var link = e.target.closest("a.bookmark");
//            var courseId = parseInt(link.getAttribute('data-courseid'), 10);
//            //console.log("Course ID:", courseId);

//            fetch('/Courses/SaveCourse', {
//                method: 'POST',
//                headers: {
//                    'Content-Type': 'application/json'
//                },
//                body: JSON.stringify({ CourseId: courseId })
//            })
//                .then(response => response.json())
//                .then(data => {
//                    if (data.success) {
//                        //var button = e.target.querySelector('.bookmark');
//                        link.classList.toggle('saved');
//                    } else {
//                        console.log("Error saving course.");
//                    }
//                })
//                .catch(error => {
//                    console.log("Error saving course:", error);
//                });
//        }
//    });
//});

//document.addEventListener('DOMContentLoaded', function () {
//    let links = document.querySelectorAll("a.button-top");
//    links.forEach(link => {
//        link.addEventListener('click', function () {
//            let courseId = parseInt(link.getAttribute('data-courseid'), 10);
//            console.log("Course ID:", courseId);
//            fetch('/courses/savecourse', {
//                method: 'POST',
//                headers: {
//                    'Content-Type': 'application/json'
//                },
//                body: JSON.stringify({ CourseId: courseId })
//            })
//                .then(response => response.json())
//                .then(data => {
//                    if (data.success) {
//                        link.classList.toggle('saved');
//                    } else {
//                        console.log("Error saving course.");
//                    }
//                })
//                .catch(error => {
//                    console.log("Error saving course:", error);
//                });
//        })
//    })

//});
//document.addEventListener('DOMContentLoaded', function () {
//    document.addEventListener('click', function (e) {
//        if (e.target && e.target.matches("a.button-top, a.button-top i")) {
//            e.preventDefault();

//            var link = e.target.closest("a.button-top");
//            var courseId = parseInt(link.getAttribute('data-courseid'), 10);
//            console.log("Course ID:", courseId);

//            fetch('/courses/savecourse', {
//                method: 'POST',
//                headers: {
//                    'Content-Type': 'application/json'
//                },
//                body: JSON.stringify({ CourseId: courseId })
//            })
//                .then(response => response.json())
//                .then(data => {
//                    if (data.success) {
//                        link.classList.toggle('saved');
//                    } else {
//                        console.log("Error saving course.");
//                    }
//                })
//                .catch(error => {
//                    console.log("Error saving course:", error);
//                });
//        }
//    });
//});

//document.addEventListener('DOMContentLoaded', function () {
//    try {
//        // Hämta referens till knappen för att spara bokmärke
//        var bookmarkButton = document.getElementById("bookmark-button");

//        // Lägg till en klickhändelse för att spara bokmärke när knappen klickas
//        bookmarkButton.addEventListener("click", function (event) {
//            event.preventDefault(); // Förhindra standardbeteendet för länken

//            var courseId = parseInt(bookmarkButton.getAttribute("asp-route-id"), 10) // Hämta kursens ID från knappen
//            console.log("Course ID:", courseId);
//            // Skapa en AJAX-förfrågan för att spara bokmärket
//            var xhr = new XMLHttpRequest();
//            xhr.open("POST", "/Courses/SaveCourse/" + courseId, true);
//            xhr.setRequestHeader("Content-Type", "application/json");

//            // Vid mottagande av svar från servern
//            xhr.onload = function () {
//                if (xhr.status === 200) {
//                    // Bokmärket sparades framgångsrikt
//                    alert("Bokmärket har sparats!");
//                } else {
//                    // Något gick fel
//                    alert("Ett fel uppstod. Försök igen senare.");
//                }
//            };

//            // Skicka förfrågan
//            xhr.send();
//        });
//    }
//    catch (error) { console.log("Error saving course:", error) }

//});

//// Hämta referens till knappen för att spara bokmärke
//var bookmarkButton = document.getElementById("bookmark-button");

//// Lägg till en klickhändelse för att spara bokmärke när knappen klickas
//bookmarkButton.addEventListener("click", function (event) {
//    event.preventDefault(); // Förhindra standardbeteendet för länken

//    var courseId = bookmarkButton.getAttribute("asp-route-id"); // Hämta kursens ID från knappen
//    console.log("Course ID:", courseId);
//    // Skicka AJAX-förfrågan med hjälp av Fetch API
//    fetch("/Courses/SaveCourse/" + courseId, {
//        method: "POST",
//        headers: {
//            "Content-Type": "application/json"
//        }
//    })
//        .then(response => {
//            if (!response.ok) {
//                throw new Error("Ett fel uppstod. Försök igen senare.");
//            }
//            return response.json();
//        })
//        .then(data => {
//            // Bokmärket sparades framgångsrikt
//            alert("Bokmärket har sparats!");
//        })
//        .catch(error => {
//            // Något gick fel
//            alert(error.message);
//        });
//});

//document.addEventListener('DOMContentLoaded', function () {
//    document.addEventListener('click', function (e) {
//        if (e.target && e.target.matches("a.bookmark, a.bookmark i")) {
//            e.preventDefault();

//            var link = e.target.closest("a.bookmark");
//            var courseId = parseInt(link.getAttribute('data-courseid'), 10);
//            //console.log("Course ID:", courseId);

//            fetch('/Courses/SaveCourse', {
//                method: 'POST',
//                headers: {
//                    'Content-Type': 'application/json'
//                },
//                body: JSON.stringify({ CourseId: courseId })
//            })
//                .then(response => response.json())
//                .then(data => {
//                    if (data.success) {
//                        //var button = e.target.querySelector('.bookmark');
//                        link.classList.toggle('saved');
//                    } else {
//                        console.log("Error saving course.");
//                    }
//                })
//                .catch(error => {
//                    console.log("Error saving course:", error);
//                });
//        }
//    });
//});

const saveCourse = (courseId) => {
    //let link = document.getElementById("a.bookmark-button");

    fetch(`/courses/savecourse/${courseId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ CourseId: courseId })
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alert("Course is now saved!");
                /*button.classList.toggle('saved');*/
            } else {
                alert(data.message);
            }
        })
        .catch(error => {
            console.log("Error saving course:", error);
        });
}

//.then(response => response.text())
//.then(data => {
//    const parser = new DOMParser()
//    const dom = parser.parseFromString(data, "text/html")
//})

//let links = document.querySelectorAll("a.bookmark-button");
//    links.forEach(link => {
//        link.addEventListener('click', function () {
//            let courseId = parseInt(link.getAttribute('data-courseid'), 10);
//            console.log("Course ID:", courseId);