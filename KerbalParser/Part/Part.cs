using System;

namespace KerbalParser
{
    public class Part
    {
        public enum PartCategory
        {
            none = -1,
            Propulsion,
            Control,
            Structural,
            Aero,
            Utility,
            Science,
            Pods,
            FuelTank,
            Engine,
            Communication,
            Electrical,
            Ground,
            Thermal,
            Payload,
            Coupling
        }

        public enum DragModel
        {
            DEFAULT,
            CONIC,
            CYLINDRICAL,
            SPHERICAL,
            CUBE,
            NONE
        }

        public enum PhysicalSignificance
        {
            FULL,
            NONE
        }

        public enum VesselType
        {
            Debris,
            SpaceObject,
            Unknown,
            Probe,
            Relay,
            Rover,
            Lander,
            Ship,
            Plane,
            Station,
            Base,
            EVA,
            Flag
        }

        public enum PartModule
        {
            Part,
            CompoundPart
        }

        public string name;

        public PartModule module;

        public string mesh;

        public double scale;

        public double rescaleFactor;

        public string texture;

        public string normalmap;

        public double specPower;

        public double rimFalloff;

        public double alphaCutoff;

        public Vector3 iconCenter;

        public string manufacturer;

        public string author;

        public string title;

        public string description;

        public PartCategory category;

        public string subcategory;

        public string TechRequired;

        public int entryCost;

        public double cost;

        public string bulkheadProfiles;

        public string tags;

        public AttachRules attachRules;

        public double mass;

        public Part.DragModel dragModelType;

        public double maximum_drag;

        public double minimum_drag;

        public double angularDrag;

        public double crashTolerance;

        public double breakingForce;

        public double breakingTorque;

        public double explosionPotential;

        public bool fuelCrossFeed;

        public string NoCrossFeedNodeKey;

        public double maxTemp;

        public double skinMaxTemp;

        public double heatConductivity;

        public double heatConvectiveConstant;

        public double emissiveConstant;

        public double thermalMassModifier;

        public double skinInternalConductionMult;

        public double radiatorHeadroom;

        public double radiatorMax;

        public double skinMassPerArea;

        public Part.PhysicalSignificance PhysicsSignificance;

        public Vector3 CoLOffset;

        public Vector3 CoMOffset;

        public Vector3 CoPOffset;

        public Vector3 CenterOfDisplacement;

        public Vector3 CenterOfBuoyancy;

        public string stagingIcon;

        public int stageOffset;

        public int childStageOffset;

        public int stackSymmetry;

        public Vector3 mirrorRefAxis;

        public int CrewCapacity;

        public Part.VesselType vesselType;

        public double buoyancy;

        public string buoyancyUseCubeNamed;

        public bool buoyancyUseSine;

        public double bodyLiftMultiplier;

        public bool inverseStageCarryover;

        public double boundsMultiplier;

        public Vector3 boundsCentroidOffset;

        public bool bodyLiftOnlyUnattachedLift;

        public string bodyLiftOnlyAttachName;

        public bool noAutoEVAMulti;

        public bool resourcePriorityUseParentInverseStage;

        public bool ActivatesEvenIfDisconnected;

        public bool skipColliderIgnores;

        public Quaternion initRotation;

        public double maxLength;

        private string _partUrl;

        private ConfigNode _internalConfig;

        private ConfigNode _config;

        private string _configFileFullName;

        private UrlConfig _urlConfig;

        private PartResourceList _resources;

        public ConfigNode Config
        {
            get
            {
                return this._config;
            }
        }

        public UrlConfig UrlConfig
        {
            get
            {
                return this._urlConfig;
            }
        }

        public string PartUrl
        {
            get
            {
                return this._partUrl;
            }
        }

        public string ConfigFileFullName
        {
            get
            {
                return this._configFileFullName;
            }
        }

        public PartResourceList Resources
        {
            get
            {
                return this._resources;
            }
        }

        public ConfigNode InternalConfig
        {
            get
            {
                return this._internalConfig;
            }
        }

        public string Mod
        {
            get
            {
                string[] array = this._partUrl.Split(new char[]
                {
                    '/'
                });
                if (array.Length >= 2)
                {
                    if (array[0] == "data")
                    {
                        return array[1];
                    }

                }
                if (array.Length >= 1)
                {
                    return array[0];
                }
                return null;
            }
        }

        public Part()
        {
            this.Init();
        }

        private void Init()
        {
            this.name = "unknownPart";
            this.module = Part.PartModule.Part;
            this.mesh = "model.mu";
            this.rescaleFactor = 1.25;
            this.scale = 1;
            this.author = "Unknown";
            this.title = "Unknown Mystery Component";
            this.manufacturer = "Found lying by the side of the road";
            this.description = "Nothing is really known about this thing. Use it at your own risk.";
            this.TechRequired = string.Empty;
            this.bulkheadProfiles = string.Empty;
            this.subcategory = "0";
            this.tags = "*";
            this.mass = 2.0;
            this.dragModelType = Part.DragModel.CUBE;
            this.maximum_drag = 0.1;
            this.minimum_drag = 0.1;
            this.angularDrag = 2.0;
            this.crashTolerance = 9.0;
            this.breakingForce = 22.0;
            this.breakingTorque = 22.0;
            this.explosionPotential = 0.5;
            this.maxTemp = 2000;
            this.skinMaxTemp = -1;
            this.heatConductivity = 0.12;
            this.heatConvectiveConstant = 1.0;
            this.emissiveConstant = 0.4;
            this.thermalMassModifier = 1.0;
            this.skinInternalConductionMult = 1.0;
            this.radiatorHeadroom = 0.25;
            this.radiatorMax = 0.25;
            this.skinMassPerArea = 1.0;
            this.fuelCrossFeed = true;
            this.buoyancy = 1.0;
            this.buoyancyUseCubeNamed = string.Empty;
            this.buoyancyUseSine = true;
            this.bodyLiftMultiplier = 1.0;
            this.iconCenter = Vector3.Zero;
            this.CoLOffset = Vector3.Zero;
            this.CoMOffset = Vector3.Zero;
            this.CoPOffset = Vector3.Zero;
            this.CenterOfBuoyancy = Vector3.Zero;
            this.CenterOfDisplacement = Vector3.Zero;
            this.boundsCentroidOffset = Vector3.Zero;
            this.boundsMultiplier = 1.0;
            this.stagingIcon = string.Empty;
            this.inverseStageCarryover = true;
            this.ActivatesEvenIfDisconnected = true;
            this.initRotation = Quaternion.Identity;
            this.maxLength = 10.0;
            this.attachRules = new AttachRules();
            this._internalConfig = new ConfigNode();
            this._resources = new PartResourceList(this);
        }

        public void Load(UrlConfig urlConfig, ConfigNode node)
        {
            this._partUrl = urlConfig.Url;
            this._urlConfig = urlConfig;
            this._configFileFullName = urlConfig.Parent.FullPath;
            this.Load(node);
        }

        private void Load(ConfigNode node)
        {
            this._config = node;
            this.name = node.GetValue("name");
            int count = node.Values.Count;
            ConfigNode.LoadObjectFromConfig(this, node, false);
            string attachRules = node.GetValue("attachRules");
            if (!string.IsNullOrEmpty(attachRules))
            {
                this.attachRules = AttachRules.Parse(attachRules);
            }
            this._internalConfig = node.GetNode("INTERNAL");
            ConfigNode[] resources = node.GetNodes("RESOURCE");
            int count2 = resources.Length;
            for (int j = 0; j < count2; j++)
            {
                this._resources.Add(resources[j]);
            }
        }

        public void Save(ConfigNode node)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
