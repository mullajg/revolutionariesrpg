import React from "react";
import { Textarea, Button, Card, CardBody } from "@heroui/react";
import { Icon } from "@iconify/react";

interface Note {
    id: string;
    title: string;
    content: string;
    date: string;
}

export default function NotesSection(){
    const [notes, setNotes] = React.useState<Note[]>([
        {
            id: "1",
            title: "Quest: The Lost Artifact",
            content: "The town elder has asked us to recover an ancient artifact from the ruins to the north. Rumors say the ruins are haunted by the spirits of fallen warriors. We should prepare accordingly.",
            date: "Session 3"
        },
        {
            id: "2",
            title: "Important NPC: Elara the Merchant",
            content: "Elara runs the general store in Oakvale. She has connections to the Thieves' Guild and might be able to get us rare items for the right price. She seems to know more about the town's politics than she lets on.",
            date: "Session 2"
        },
        {
            id: "3",
            title: "Mysterious Symbol",
            content: "We found a strange symbol carved into the trees near the abandoned mine. It looks like a crescent moon with three stars. Need to ask the local sage about its meaning.",
            date: "Session 4"
        }
    ]);

    const [newNote, setNewNote] = React.useState<Omit<Note, "id" | "date">>({
        title: "",
        content: ""
    });

    const [editingNote, setEditingNote] = React.useState<Note | null>(null);
    const [showAddForm, setShowAddForm] = React.useState(false);

    const handleAddNote = () => {
        if (newNote.title && newNote.content) {
            const date = new Date().toLocaleDateString();
            setNotes([...notes, {
                id: Date.now().toString(),
                title: newNote.title,
                content: newNote.content,
                date: `Session ${notes.length + 1}`
            }]);
            setNewNote({ title: "", content: "" });
            setShowAddForm(false);
        }
    };

    const handleRemoveNote = (id: string) => {
        setNotes(notes.filter(note => note.id !== id));
    };

    const handleEditNote = (note: Note) => {
        setEditingNote(note);
    };

    const handleSaveEdit = () => {
        if (editingNote) {
            setNotes(notes.map(note =>
                note.id === editingNote.id ? editingNote : note
            ));
            setEditingNote(null);
        }
    };

    const handleCancelEdit = () => {
        setEditingNote(null);
    };

    return (
        <div className="space-y-6">
            <div className="flex justify-between items-center">
                <h2 className="text-xl font-semibold text-slate-800 dark:text-slate-200">Notes</h2>
                <Button
                    color="primary"
                    onPress={() => setShowAddForm(!showAddForm)}
                    startContent={<Icon icon={showAddForm ? "lucide:minus" : "lucide:plus"} />}
                >
                    {showAddForm ? "Cancel" : "Add Note"}
                </Button>
            </div>

            {showAddForm && (
                <Card>
                    <CardBody className="p-4">
                        <div className="space-y-4">
                            <Input
                                label="Title"
                                placeholder="Enter note title"
                                value={newNote.title}
                            />
                            <Textarea
                                label="Content"
                                placeholder="Enter note content"
                                value={newNote.content}
                                onValueChange={(value) => setNewNote({ ...newNote, content: value })}
                                minRows={4}
                            />
                            <div className="flex justify-end">
                                <Button
                                    color="primary"
                                    onPress={handleAddNote}
                                >
                                    Add Note
                                </Button>
                            </div>
                        </div>
                    </CardBody>
                </Card>
            )}

            <div className="space-y-4">
                {notes.map((note) => (
                    <Card key={note.id}>
                        <CardBody className="p-4">
                            {editingNote && editingNote.id === note.id ? (
                                <div className="space-y-4">
                                    <Input
                                        label="Title"
                                        value={editingNote.title}
                                    />
                                    <Textarea
                                        label="Content"
                                        value={editingNote.content}
                                        onValueChange={(value) => setEditingNote({ ...editingNote, content: value })}
                                        minRows={4}
                                    />
                                    <div className="flex justify-end gap-2">
                                        <Button
                                            color="default"
                                            variant="flat"
                                            onPress={handleCancelEdit}
                                        >
                                            Cancel
                                        </Button>
                                        <Button
                                            color="primary"
                                            onPress={handleSaveEdit}
                                        >
                                            Save
                                        </Button>
                                    </div>
                                </div>
                            ) : (
                                <div>
                                    <div className="flex justify-between items-start mb-2">
                                        <div>
                                            <h3 className="text-lg font-semibold">{note.title}</h3>
                                            <p className="text-xs text-slate-500 dark:text-slate-400">{note.date}</p>
                                        </div>
                                        <div className="flex gap-2">
                                            <Button
                                                isIconOnly
                                                size="sm"
                                                variant="light"
                                                onPress={() => handleEditNote(note)}
                                            >
                                                <Icon icon="lucide:edit" className="w-4 h-4" />
                                            </Button>
                                            <Button
                                                isIconOnly
                                                size="sm"
                                                color="danger"
                                                variant="light"
                                                onPress={() => handleRemoveNote(note.id)}
                                            >
                                                <Icon icon="lucide:trash-2" className="w-4 h-4" />
                                            </Button>
                                        </div>
                                    </div>
                                    <p className="text-slate-600 dark:text-slate-300 whitespace-pre-wrap">{note.content}</p>
                                </div>
                            )}
                        </CardBody>
                    </Card>
                ))}
            </div>
        </div>
    );
}

// Import needed for the Input component
const Input = ({ label, ...props }: {
    label?: string,
    [key: string]: any
}) => {
    return (
        <div className="flex flex-col gap-1.5">
            {label && <label className="text-sm font-medium text-slate-700 dark:text-slate-300">{label}</label>}
            <input
                className="bg-transparent border border-slate-300 dark:border-slate-600 rounded-md px-3 py-1.5 text-sm"
                {...props}
            />
        </div>
    );
};