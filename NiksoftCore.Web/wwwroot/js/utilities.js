// JavaScript source code
function previewFile() {
	const preview = document.querySelector("#face");
	const preview2 = document.querySelector("#face2");
	const file = document.querySelector("#fileFace").files[0];
	const reader = new FileReader();

	reader.addEventListener("load", function () {
		preview.src = reader.result;
		preview2.src = reader.result;
	}, false);

	if (file) {
		reader.readAsDataURL(file);
	}
};
function previewFile2() {
	const preview = document.querySelector("#signature");
	const preview2 = document.querySelector("#signature2");
	const file = document.querySelector("#fileSignature").files[0];
	const reader = new FileReader();

	reader.addEventListener("load", function () {
		preview.src = reader.result;
		preview2.src = reader.result;
	}, false);

	if (file) {
		reader.readAsDataURL(file);
	}
};