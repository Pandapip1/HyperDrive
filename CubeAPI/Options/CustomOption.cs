using System;

namespace AllOfUs.CubeAPI.Options
{
    public abstract class CustomOption
    {
        public string Title;
        public CustomStringName StringName { get; protected set; }
        protected virtual float PrivateFloatValue => throw new NotImplementedException();
        protected virtual int PrivateIntValue => throw new NotImplementedException();
        protected virtual bool PrivateBoolValue => throw new NotImplementedException();
        protected virtual string PrivateStringValue => throw new NotImplementedException(); 
    }
}
