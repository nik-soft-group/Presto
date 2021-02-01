
var rootObjects = [];

function getInstance(vueObj, index) {
	var instance = new Vue(vueObj);
	rootObjects[index] = instance;
	return instance;
}

var fileIcons = {
	image: '<i class="fa fa-file-image-o" aria-hidden="true"></i>',
	video: '<i class="fa fa-file-video-o" aria-hidden="true"></i>',
	sound: '<i class="fa fa-file-audio-o" aria-hidden="true"></i>',
	other: '<i class="fa fa-file-archive-o" aria-hidden="true"></i>'
};

var fileExtentions = {
	image: ['jpg', 'png', 'jepeg', 'gif'],
	video: ['mp4', 'mwv', '3gp'],
	sound: ['pm3', '3ga'],
	other: ['pdf', 'doc', 'xlsx', 'xls']
}

//mixins: [utilities]
var utilities = {
	methods: {
		loadJson: function (apiurl, apidata) {
			this.loading = true;
			return fetch(apiurl, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json;charset=utf-8'
				},
				body: JSON.stringify(apidata)
			}).finally(() => {
					this.loading = false;
				}).then(response => response.json());
		},
		pdate: function (thed, withhour, formatstring) {
			if (thed === undefined || thed === null) {
				return "";
			}

			if (formatstring != undefined) {
				return new persianDate(thed).format(formatstring);
			}

			if (withhour) {
				return new persianDate(thed).format('YYYY/MM/DD HH:mm');
			}
			return new persianDate(thed).format('YYYY/MM/DD');
		},
		faToEnNumbers: function (str) {
			if (str === undefined || str == null || str === '') {
				return '';
			}
			return str.toString().replace(/[۰-۹]/g, d => '۰۱۲۳۴۵۶۷۸۹'.indexOf(d));
		},
		enToFaNumbers: function (str) {
			if (str === undefined || str == null || str === '') {
				return '';
			}
			return str.toString().replace(/\d/g, d => '۰۱۲۳۴۵۶۷۸۹'[d]);
		},
		getIconByFileUrl: function (url) {
			var lex = url.lastIndexOf('.');
			var extention = url.substring(lex + 1, url.length).toLowerCase();
			console.log(extention);
			var result = '';
			if (fileExtentions.image.includes(extention)) {
				result = fileIcons.image;
			} else if (fileExtentions.video.includes(extention)) {
				result = fileIcons.video;
			} else if (fileExtentions.sound.includes(extention)) {
				result = fileIcons.sound;
			} else if (fileExtentions.other.includes(extention)) {
				result = fileIcons.other;
			}

			return result;
		},
		addSeperator: function (str) {
			if (str === undefined || str == null || str === '') {
				return '';
			}
			return this.completeReplace(str.toString(), ',', '').replace(/\B(?=(\d{3})+(?!\d))/g, ",");
		},
		completeReplace: function (str, find, replace) {
			if (str === undefined || str == null || str === '') {
				return '';
			}
			return str.replace(new RegExp(find, 'g'), replace);
		},
		GetFileName: function (FilePath) {
			if (FilePath === undefined || FilePath === null) {
				return "";
			}
			p = FilePath.lastIndexOf("/");
			if (p == -1) {
				return "";
			}
			return FilePath.substring(p + 1)
		},
		checkDateControls: function (ids) {
			$.each(ids, function (ix, vl) {
				setDateControl('#' + vl);
			});
		},
		stringLimit: function (str, len) {
			if (str.length > len) {
				return { value: str.substring(0, len) + " ...", isMore: true };
			} else {
				return { value: str, isMore: false };
			}
		},
		showMessage: function(input) {
			new Noty({
				text: input.message,
				layout: 'topRight',
				type: input.type,
				theme: 'sunset',
				timeout: 3000
			}).show();
		}
	}
}
