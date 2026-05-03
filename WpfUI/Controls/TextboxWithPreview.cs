using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfUI.Controls;

public class TextboxWithPreview : TextBox
{
    static TextboxWithPreview()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TextboxWithPreview), new FrameworkPropertyMetadata(typeof(TextboxWithPreview)));
    }

    public static DependencyProperty TextPreviewProperty = DependencyProperty.Register(
        "TextPreview",
        typeof(string),
        typeof(TextboxWithPreview));

    public string TextPreview
    {
        get => (string)GetValue(TextPreviewProperty);
        set => SetValue(TextPreviewProperty, value);
    }

    public static readonly DependencyProperty HasTextProperty =
        DependencyProperty.Register("HasText",
            typeof(bool),
            typeof(TextboxWithPreview),
            new FrameworkPropertyMetadata(false,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                | FrameworkPropertyMetadataOptions.AffectsRender));

    public bool HasText
    {
        get => (bool)GetValue(HasTextProperty);
        set => SetValue(HasTextProperty, value);
    }

    protected override void OnTextInput(TextCompositionEventArgs e)
    {
        HasText = string.IsNullOrEmpty(e.Text) == false;
        base.OnTextInput(e);
    }

    protected override void OnTextChanged(TextChangedEventArgs e)
    {
        HasText = string.IsNullOrEmpty(Text) == false;
        base.OnTextChanged(e);
    }
}
