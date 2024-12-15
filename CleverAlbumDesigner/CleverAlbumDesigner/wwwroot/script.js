
document.addEventListener("DOMContentLoaded", function () {
    const uploadForm = document.getElementById("uploadForm");
    const fileInput = document.getElementById("fileInput");
    const photosContainer = document.getElementById("photosContainer");
    const errorContainer = document.getElementById("errorContainer");
    const apiBaseUrl = "http://ec2-13-48-55-131.eu-north-1.compute.amazonaws.com/api";//Url Base
    let sessionId = sessionStorage.getItem("SessionId");
    if (!sessionId) {
        // Generate a new SessionId and store it in localStorage
        sessionId = generateSessionId();
        sessionStorage.setItem("SessionId", sessionId);
    }
    function generateSessionId() {
        const timestamp = Date.now().toString(36); // Parte basada en el tiempo
        const randomPart = Math.random().toString(36).substring(2, 15); // Parte aleatoria
        return `${timestamp}-${randomPart}`;
    }

    // Upload photos
    uploadForm.addEventListener("submit", async (e) => {
        e.preventDefault();
        errorContainer.textContent = ""; // Clean previous error messages

        const file = fileInput.files[0];
        if (!file) {
            errorContainer.textContent = "Please, select a file";
            return;
        }

        const formData = new FormData();
        formData.append("file", file);

        try {
            const response = await fetch(`${apiBaseUrl}/photo/upload`, {
                method: "POST",
                body: formData,
                headers: {
                    "SessionId": sessionId
                },
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(`Error: ${errorText}`);
            }

            showToast("File uploaded succesfully.");
            fileInput.value = ""; // Clean input file
            fetchPhotos(); // Reload Photos
        } catch (error) {
            console.error(error);
            errorContainer.textContent = error.message;
            showToast(error.message, "danger")
        }
    });

    // List Photos
    async function fetchPhotos() {
        photosContainer.innerHTML = ""; // Clear previous photos

        try {
            const response = await fetch(`${apiBaseUrl}/photo/allunassigned`, {
                method: "GET",
                headers: {
                    "SessionId": sessionId
                },
            });
            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(`Error: ${errorText}`);
            }

            const photos = await response.json();
            if (photos.length === 0) {
                photosContainer.textContent = "There are no photos to show.";
                return;
            }

            photos.forEach((photo) => {
                // Create the container for each photo
                const photoDiv = document.createElement("div");
                photoDiv.className = "position-relative m-2";
                photoDiv.style.width = "200px";

                // Add image
                const imgElement = document.createElement("img");
                imgElement.src = photo.preSignedUrl; // Image URL
                imgElement.className = "img-thumbnail";
                imgElement.style.width = "100%";
                imgElement.style.height = "auto";

                // Add delete button
                const deleteButton = document.createElement("button");
                deleteButton.className = "btn btn-danger btn-delete-photo position-absolute top-0 end-0 m-1";
                deleteButton.style.opacity = "0.8";
                deleteButton.style.borderRadius = "50%";
                deleteButton.innerHTML = '<i class="bi bi-trash"></i>';
                deleteButton.onclick = () => deletePhotoConfirm(photo.photoId);

                // add image and button to the container
                photoDiv.appendChild(imgElement);
                photoDiv.appendChild(deleteButton);

                // add photoDiv to photosContainer
                photosContainer.appendChild(photoDiv);
            });
        } catch (error) {
            console.error("Error fetching photos:", error);
            photosContainer.textContent = error.message || "Error loading photos.";
        }
    }

    function deletePhotoConfirm(photoId) {
        showConfirmModal("Are you sure you want to delete this photo?", () => {
            deletePhoto(photoId);
        });
    }

    // Delete photo
    async function deletePhoto(photoId) {

        try {
            const response = await fetch(`${apiBaseUrl}/photo/delete/${photoId}`, {
                method: "DELETE",
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(`Error: ${errorText}`);
            }

            showToast("Photo was succesfully removed");
            fetchPhotos(); //Reload Photos after deletion
        } catch (error) {
            console.error(error);
            showToast(error.message, "danger");
        }
    }
    // Event listener to generate the album
    document.getElementById("generateAlbumButton").addEventListener("click", async () => {
        const themeSelect = document.getElementById("themeSelect");
        const albumErrorContainer = document.getElementById("albumErrorContainer");
        const albumSuffixInput = document.getElementById("albumSuffix");

        albumErrorContainer.textContent = ""; // Clear previous error messages

        const theme = themeSelect.value;
        const suffix = albumSuffixInput.value.trim();
        if (!theme) {
            albumErrorContainer.textContent = "Please select a theme";
            return;
        }

        try {
            // Generate the album
            const response = await fetch(`${apiBaseUrl}/albums/generate/${theme}?suffix=${encodeURIComponent(suffix)}`, {
                method: "POST",
                headers: {
                    "SessionId": sessionId
                },
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(`Error: ${errorText}`);
            }

            const result = await response.json()
            showToast(result.message);
            fetchPhotos();
            fetchAlbums();
        } catch (error) {
            console.error("Error generating album:", error);
            showToast("Error generating album.","danger");
        }
    });


    async function fetchAlbums() {
        const albumsContainer = document.getElementById("albumsContainer");
        albumsContainer.textContent = "No albums available."; 
        albumPhotosContainer.textContent = "No album photos to display."; // Reset album photos display

        try {
            const response = await fetch(`${apiBaseUrl}/albums/all`, {
                method: "GET",
                headers: {
                    "SessionId": sessionId
                },
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(`Error: ${errorText}`);
            }

            const albums = await response.json();
            if (albums.length === 0) {
                // Show a message if there are no albums
                albumsContainer.textContent = "No albums available.";
                return;
            }
            
            albumsContainer.innerHTML = "";

            // Create a row to hold the cards
            const rowDiv = document.createElement("div");
            rowDiv.className = "row g-3"; // Bootstrap grid with spacing between items

            // Render each album as a card within a column         
            albums.forEach((album) => {
                const albumDiv = document.createElement("div");
                albumDiv.className = "col-md-4 mb-3"; // Each card takes 4 columns

                const cardDiv = document.createElement("div");
                cardDiv.className = "card p-3"; 
                cardDiv.style.maxWidth = "300px"; // Limit card width

                const albumLabel = document.createElement("h5");
                albumLabel.className = "card-title text-center"; // Center the title
                albumLabel.textContent = `Album: ${album.name}`;

                // Button container
                const buttonContainer = document.createElement("div");
                buttonContainer.className = "d-flex justify-content-between mt-3"; // Align buttons in a row

                const viewButton = document.createElement("button");
                viewButton.textContent = "View Album";
                viewButton.className = "btn btn-primary";
                viewButton.addEventListener("click", () => fetchAlbumPhotos(album.albumId));

                const deleteButton = document.createElement("button");
                deleteButton.textContent = "Delete Album";
                deleteButton.className = "btn btn-danger";
                deleteButton.addEventListener("click", () => deleteAlbumConfirm(album.albumId));

                const downloadButton = document.createElement("button");
                downloadButton.textContent = "Download Album";
                downloadButton.className = "btn btn-success";
                downloadButton.addEventListener("click", () => downloadAlbum(album.albumId));               

                // Append buttons to container
                buttonContainer.appendChild(viewButton);
                buttonContainer.appendChild(deleteButton);
                buttonContainer.appendChild(downloadButton);

                // Append elements to the card
                cardDiv.appendChild(albumLabel);
                cardDiv.appendChild(buttonContainer);

                // Append the card to the column
                albumDiv.appendChild(cardDiv);
                albumsContainer.appendChild(albumDiv);
            });

            albumsContainer.className = "row g-3"; 

            // Add the row to the container
            albumsContainer.appendChild(rowDiv);
        } catch (error) {
            console.error("Error fetching albums:", error);
            albumsContainer.textContent = error.message || "Error loading albums."; 
        }
    }

    async function fetchAlbumPhotos(albumId) {
        const albumPhotosContainer = document.getElementById("albumPhotosContainer");
        albumPhotosContainer.innerHTML = "Loading album photos...";

        try {
            const response = await fetch(`${apiBaseUrl}/albums/${albumId}/photos`, {
                method: "GET",
                headers: {
                    "SessionId": sessionId
                },
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(`Error: ${errorText}`);
            }

            const photos = await response.json();

            // Check if there are no photos
            if (photos.length === 0) {
                albumPhotosContainer.textContent = "No photos found in this album.";
                return;
            }

            // Render the photos
            albumPhotosContainer.innerHTML = ""; // Clear previous photos
            photos.forEach((photo) => {
                const imgElement = document.createElement("img");
                imgElement.src = photo.preSignedUrl; // Signed URL for the photo
                imgElement.alt = `Photo ${photo.photoId}`;
                imgElement.style.width = "200px";
                imgElement.style.margin = "10px";

                albumPhotosContainer.appendChild(imgElement);
            });
        } catch (error) {
            console.error("Error fetching album photos:", error);
            albumPhotosContainer.textContent = error.message || "Error loading album photos.";
        }
    }

    function deleteAlbumConfirm(albumId) {
        showConfirmModal("Are you sure you want to delete this album and all its photos?", () => {
            deleteAlbum(albumId);
        });
    }

    async function deleteAlbum(albumId) {
        try {
            const response = await fetch(`${apiBaseUrl}/albums/${albumId}`, { method: "DELETE" });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(`Error: ${errorText}`);
            }

            showToast("Album and its photos deleted successfully.");
            fetchAlbums(); // Refresh album list
        } catch (error) {
            console.error("Error deleting album:", error);
            showToast(error.message,"danger");
        }
    }

    function showConfirmModal(message, onConfirm) {
        const modalMessage = document.getElementById("confirmModalMessage");
        modalMessage.textContent = message;

        const confirmButton = document.getElementById("confirmDeleteButton");
        confirmButton.onclick = () => {
            onConfirm();
            const confirmModal = bootstrap.Modal.getInstance(document.getElementById("confirmModal"));
            confirmModal.hide();
        };

        const confirmModal = new bootstrap.Modal(document.getElementById("confirmModal"));
        confirmModal.show();
    }

    function showToast(message, type = "primary") {
        const toastMessage = document.getElementById("toastMessage");
        toastMessage.textContent = message;

        const toastElement = document.getElementById("dynamicToast");
        toastElement.className = `toast align-items-center text-bg-${type} border-0`;

        const toast = new bootstrap.Toast(toastElement);
        toast.show();
    }

    async function downloadAlbum(albumId) {
        try {
            const response = await fetch(`${apiBaseUrl}/albums/download/${albumId}`, {
                method: "GET",
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(`Error: ${errorText}`);
            }

            // Crear un a blob for download
            const blob = await response.blob();
            const url = window.URL.createObjectURL(blob);

            // Crear a link to download the file
            const a = document.createElement("a");
            a.href = url;
            document.body.appendChild(a);
            a.click();

            // clean Url
            a.remove();
            window.URL.revokeObjectURL(url);
        } catch (error) {
            console.error("Error downloading album:", error);
            alert(error.message || "Error downloading album");
        }
    }

    fetchPhotos();
    fetchAlbums();
});