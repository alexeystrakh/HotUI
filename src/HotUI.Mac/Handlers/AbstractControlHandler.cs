﻿using System;
using AppKit;
using HotUI.Mac.Controls;

namespace HotUI.Mac.Handlers
{
    public abstract class AbstractControlHandler<TVirtualView, TNativeView> : MacViewHandler 
        where TVirtualView : View 
        where TNativeView: NSView
    {
        private PropertyMapper<TVirtualView> _mapper;
        private TVirtualView _virtualView;
        private TNativeView _nativeView;
        private HUIContainerView _containerView;
        
        public event EventHandler<ViewChangedEventArgs> NativeViewChanged;

        protected AbstractControlHandler()
        {
        }
        
        protected AbstractControlHandler(PropertyMapper<TVirtualView> mapper)
        {
            _mapper = mapper;
        }

        protected void SetMapper(PropertyMapper<TVirtualView> mapper)
        {
            _mapper = mapper;
        }
        
        protected abstract TNativeView CreateView();
        
        protected abstract void DisposeView(TNativeView nativeView);

        public NSView View => (NSView)_containerView ?? _nativeView;

        public HUIContainerView ContainerView => _containerView;
        
        public object NativeView => _nativeView;

        public TNativeView TypedNativeView => _nativeView;
        
        protected TVirtualView VirtualView => _virtualView;
        
        public bool HasContainer
        {
            get => _containerView != null;
            set
            {
                if (!value && _containerView != null)
                {
                    var oldView = _containerView;
                    _containerView.ShadowLayer = null;
                    _containerView.MaskLayer = null;
                    _containerView = null;

                    _nativeView.RemoveFromSuperview();
                    NativeViewChanged?.Invoke(this, new ViewChangedEventArgs(VirtualView, oldView, _nativeView));
                    return;
                }

                if (value && _containerView == null)
                {
                    _containerView = new HUIContainerView();
                    _containerView.MainView = _nativeView;
                    NativeViewChanged?.Invoke(this, new ViewChangedEventArgs(VirtualView, _nativeView, _containerView));
                }
            }
        }

        public virtual SizeF Measure(SizeF availableSize)
        {
            if (_nativeView is NSControl control)
                return control.SizeThatFits(availableSize.ToCGSize()).ToSizeF();

            return HotUI.View.IllTakeWhatYouCanGive;
        }

        public void SetFrame(RectangleF frame)
        {
            View.Frame = frame.ToCGRect();
        }

        public virtual void Remove(View view)
        {
            _virtualView = null;

            // If a container view is being used, then remove the native view from it and get rid of it.
            if (_containerView != null)
            {
                _nativeView.RemoveFromSuperview();
                _containerView = null;
            }
        }
        
        public virtual void SetView(View view)
        {
            _virtualView = view as TVirtualView;
            if (_nativeView == null)
                _nativeView = CreateView();
            _mapper?.UpdateProperties(this, _virtualView);
        }

        public virtual void UpdateValue(string property, object value)
        {
            _mapper?.UpdateProperty(this, _virtualView, property);
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            _nativeView?.RemoveFromSuperview();
            
            if (_nativeView != null)
                DisposeView(_nativeView);
            
            _nativeView?.Dispose();
            _nativeView = null;
            if (_virtualView != null)
                Remove(_virtualView);

        }
        void OnDispose(bool disposing)
        {
            if (disposedValue)
                return;
            disposedValue = true;
            Dispose(disposing);
        }

        ~AbstractControlHandler()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            OnDispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            OnDispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
