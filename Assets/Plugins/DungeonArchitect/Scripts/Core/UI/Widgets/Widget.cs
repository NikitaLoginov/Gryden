﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonArchitect.UI.Widgets
{
    public delegate void WidgetDragEvent(Event e, UISystem uiSystem);

    public enum WidgetMessage
    {
        DrawHighlight
    }

    public interface IWidget
    {
        void UpdateWidget(UISystem uiSystem, Rect bounds);
        void Draw(UISystem uiSystem, UIRenderer renderer);
        void HandleInput(Event e, UISystem uiSystem);
        void HandleMessage(UISystem uiSystem, WidgetMessage message);

        bool IsCompositeWidget();
        bool CanAcquireFocus();
        bool RequiresInputEveryFrame();
        IWidget[] GetChildWidgets();
        Vector2 GetDesiredSize(Vector2 size, UISystem uiSystem);
        Rect WidgetBounds { get; set; }
        bool ShowFocusHighlight { get; set; }
        Vector2 ScrollPosition { get; set; }

    }

    public abstract class WidgetBase : IWidget
    {
        private bool showFocusHighlight = false;
        private Rect widgetBounds = Rect.zero;
        private Vector2 scrollPosition = Vector2.zero;

        public bool ShowFocusHighlight
        {
            get { return showFocusHighlight; }
            set { showFocusHighlight = value; }
        }

        public Rect WidgetBounds
        {
            get { return widgetBounds; }
            set { widgetBounds = value; }
        }

        public virtual Vector2 ScrollPosition
        {
            get { return scrollPosition; }
            set { scrollPosition = value; }
        }

        public bool DragDropEnabled = false;
        public virtual bool CanAcquireFocus() { return false; }
        public virtual bool RequiresInputEveryFrame() { return false; }
        public virtual Vector2 GetDesiredSize(Vector2 size, UISystem uiSystem) { return size; }

        public abstract void Draw(UISystem uiSystem, UIRenderer renderer);

        public virtual void UpdateWidget(UISystem uiSystem, Rect bounds)
        {
            WidgetBounds = bounds;
        }


        public virtual void HandleMessage(UISystem uiSystem, WidgetMessage message) {
            if (message == WidgetMessage.DrawHighlight)
            {
                //DrawFocusHighlight(uiSystem);
            }
        }

        protected virtual void DrawFocusHighlight(UISystem uiSystem, UIRenderer renderer)
        {
            var hilightBounds = new Rect(new Vector2(0.5f, 0.5f), WidgetBounds.size - Vector2.one);
            WidgetUtils.DrawWidgetFocusHighlight(renderer, hilightBounds, WidgetUtils.FOCUS_HIGHLITE_COLOR);
        }

        public virtual void HandleInput(Event e, UISystem uiSystem)
        {
            switch (e.type)
            {
                case EventType.MouseDrag:
                    if (DragDropEnabled)
                    {
                        HandleDragStart(e, uiSystem);
                    }
                    break;

                case EventType.DragUpdated:
                    if (DragDropEnabled && IsDragDataSupported(e, uiSystem))
                    {
                        HandleDragUpdate(e, uiSystem);
                    }
                    break;

                case EventType.DragPerform:
                    if (DragDropEnabled && IsDragDataSupported(e, uiSystem))
                    {
                        HandleDragPerform(e, uiSystem);
                    }
                    break;
            }
        }

        public virtual bool IsCompositeWidget() { return false; }
        public virtual IWidget[] GetChildWidgets() { return null; }

        protected virtual bool IsDragDataSupported(Event e, UISystem uiSystem) { return false; }

        public event WidgetDragEvent DragStart;
        public event WidgetDragEvent DragUpdate;
        public event WidgetDragEvent DragPerform;

        protected virtual void HandleDragStart(Event e, UISystem uiSystem)
        {
            uiSystem.Platform.DragDrop.PrepareStartDrag();
            uiSystem.Platform.DragDrop.StartDrag("Widget Drag");

            // Make sure no one uses the event after us
            Event.current.Use();

            if (DragStart != null)
            {
                DragStart.Invoke(e, uiSystem);
            }
        }

        void HandleDragUpdate(Event e, UISystem uiSystem)
        {
            uiSystem.Platform.DragDrop.SetVisualMode(UIDragDropVisualMode.Copy);

            if (DragUpdate != null)
            {
                DragUpdate.Invoke(e, uiSystem);
            }
        }

        void HandleDragPerform(Event e, UISystem uiSystem)
        {
            uiSystem.Platform.DragDrop.AcceptDrag();

            if (DragPerform != null)
            {
                DragPerform.Invoke(e, uiSystem);
            }
        }


    }

    public class NullWidget : WidgetBase
    {
        public override void Draw(UISystem uiSystem, UIRenderer renderer) { }
    }
}

