// Build+ Unity Extension
// Copyright (c) 2012 Luminary Productions, Inc.
// Please direct any bugs/comments/suggestions to http://luminaryproductions.net

using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Trismegistus.BuildPlus {
	[Serializable]
	public struct EditorSettings {
		public bool saveToPlayerSettings;
		public bool saveToPackageJson;
		public string packageJsonPath;
		public string buildPathScheme;
	}

	[Serializable]
	public class Build : XmlSerializableData<Build> {
		public int xmlVersion;
		public List<Version> versions = new List<Version>();
		public EditorSettings editorSettings;

		public Build() {
			editorSettings = new EditorSettings {
				saveToPlayerSettings = true,
				buildPathScheme = "{p_path}/Build/{p_name}_{ver}"
			};
		}

		[XmlIgnore]
		public string releaseNotes; // Set at build time and is a text version of everything for the BuildPlusSO

		public Version CurrentVersion {
			get {
				if (versions.Count > 0)
					return versions[0];

				return null;
			}
		}

		public override void PreSerialize() {
			base.PreSerialize();
			foreach (Version v in versions)
				v.PreSerialize();
		}
	}
}