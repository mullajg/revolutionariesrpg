import React from "react";
import { Card, CardBody, Input, Button, Textarea } from "@heroui/react";
import { Icon } from "@iconify/react";

interface Feature {
    id: string;
    name: string;
    source: string;
    description: string;
    type: "class" | "race" | "background" | "feat";
}

export default function FeaturesSection(){
    const [features, setFeatures] = React.useState<Feature[]>([
        {
            id: "1",
            name: "Second Wind",
            source: "Fighter",
            description: "You have a limited well of stamina that you can draw on to protect yourself from harm. On your turn, you can use a bonus action to regain hit points equal to 1d10 + your fighter level. Once you use this feature, you must finish a short or long rest before you can use it again.",
            type: "class"
        },
        {
            id: "2",
            name: "Action Surge",
            source: "Fighter",
            description: "Starting at 2nd level, you can push yourself beyond your normal limits for a moment. On your turn, you can take one additional action. Once you use this feature, you must finish a short or long rest before you can use it again.",
            type: "class"
        },
        {
            id: "3",
            name: "Darkvision",
            source: "Half-Elf",
            description: "Thanks to your elf blood, you have superior vision in dark and dim conditions. You can see in dim light within 60 feet of you as if it were bright light, and in darkness as if it were dim light. You can't discern color in darkness, only shades of gray.",
            type: "race"
        },
        {
            id: "4",
            name: "Military Rank",
            source: "Soldier Background",
            description: "You have a military rank from your career as a soldier. Soldiers loyal to your former military organization still recognize your authority and influence, and they defer to you if they are of a lower rank.",
            type: "background"
        },
        {
            id: "5",
            name: "Great Weapon Master",
            source: "Feat",
            description: "You've learned to put the weight of a weapon to your advantage, letting its momentum empower your strikes. You gain a +10 damage bonus to the attack's damage if you take a -5 penalty to the attack roll. Before you make a melee attack with a heavy weapon that you are proficient with, you can choose to take a -5 penalty to the attack roll. If the attack hits, you add +10 to the attack's damage.",
            type: "feat"
        },
    ]);

    const [newFeature, setNewFeature] = React.useState<Feature>({
        id: "", name: "", source: "", description: "", type: "class"
    });

    const [expandedFeatureId, setExpandedFeatureId] = React.useState<string | null>(null);
    const [showAddForm, setShowAddForm] = React.useState(false);
    const [filter, setFilter] = React.useState<Feature["type"] | "all">("all");

    const handleAddFeature = () => {
        if (newFeature.name) {
            setFeatures([...features, { ...newFeature, id: Date.now().toString() }]);
            setNewFeature({
                id: "", name: "", source: "", description: "", type: "class"
            });
            setShowAddForm(false);
        }
    };

    const handleRemoveFeature = (id: string) => {
        setFeatures(features.filter(feature => feature.id !== id));
    };

    const toggleExpandFeature = (id: string) => {
        setExpandedFeatureId(expandedFeatureId === id ? null : id);
    };

    const filteredFeatures = filter === "all"
        ? features
        : features.filter(feature => feature.type === filter);

    const getTypeIcon = (type: Feature["type"]) => {
        switch (type) {
            case "class": return "lucide:sword";
            case "race": return "lucide:users";
            case "background": return "lucide:book";
            case "feat": return "lucide:star";
            default: return "lucide:star";
        }
    };

    const getTypeColor = (type: Feature["type"]) => {
        switch (type) {
            case "class": return "primary";
            case "race": return "success";
            case "background": return "secondary";
            case "feat": return "warning";
            default: return "default";
        }
    };

    return (
        <div className="space-y-6">
            <div className="flex justify-between items-center">
                <h2 className="text-xl font-semibold text-slate-800 dark:text-slate-200">Features & Feats</h2>
                <Button
                    color="primary"
                    onPress={() => setShowAddForm(!showAddForm)}
                    startContent={<Icon icon={showAddForm ? "lucide:minus" : "lucide:plus"} />}
                >
                    {showAddForm ? "Cancel" : "Add Feature"}
                </Button>
            </div>

            <div className="flex gap-2 overflow-x-auto pb-2">
                <Button
                    size="sm"
                    color={filter === "all" ? "primary" : "default"}
                    variant={filter === "all" ? "solid" : "bordered"}
                    onPress={() => setFilter("all")}
                >
                    All
                </Button>
                <Button
                    size="sm"
                    color={filter === "class" ? "primary" : "default"}
                    variant={filter === "class" ? "solid" : "bordered"}
                    onPress={() => setFilter("class")}
                    startContent={<Icon icon="lucide:sword" className="w-4 h-4" />}
                >
                    Class
                </Button>
                <Button
                    size="sm"
                    color={filter === "race" ? "success" : "default"}
                    variant={filter === "race" ? "solid" : "bordered"}
                    onPress={() => setFilter("race")}
                    startContent={<Icon icon="lucide:users" className="w-4 h-4" />}
                >
                    Race
                </Button>
                <Button
                    size="sm"
                    color={filter === "background" ? "secondary" : "default"}
                    variant={filter === "background" ? "solid" : "bordered"}
                    onPress={() => setFilter("background")}
                    startContent={<Icon icon="lucide:book" className="w-4 h-4" />}
                >
                    Background
                </Button>
                <Button
                    size="sm"
                    color={filter === "feat" ? "warning" : "default"}
                    variant={filter === "feat" ? "solid" : "bordered"}
                    onPress={() => setFilter("feat")}
                    startContent={<Icon icon="lucide:star" className="w-4 h-4" />}
                >
                    Feats
                </Button>
            </div>

            {showAddForm && (
                <Card>
                    <CardBody className="p-4">
                        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                            <Input
                                label="Feature Name"
                                placeholder="Enter feature name"
                                value={newFeature.name}
                                onValueChange={(value) => setNewFeature({ ...newFeature, name: value })}
                            />
                            <Input
                                label="Source"
                                placeholder="Class, race, background, etc."
                                value={newFeature.source}
                                onValueChange={(value) => setNewFeature({ ...newFeature, source: value })}
                            />
                            <div className="md:col-span-2">
                                <div className="flex gap-2 mb-4">
                                    <Button
                                        size="sm"
                                        color={newFeature.type === "class" ? "primary" : "default"}
                                        variant={newFeature.type === "class" ? "solid" : "bordered"}
                                        onPress={() => setNewFeature({ ...newFeature, type: "class" })}
                                        startContent={<Icon icon="lucide:sword" className="w-4 h-4" />}
                                    >
                                        Class Feature
                                    </Button>
                                    <Button
                                        size="sm"
                                        color={newFeature.type === "race" ? "success" : "default"}
                                        variant={newFeature.type === "race" ? "solid" : "bordered"}
                                        onPress={() => setNewFeature({ ...newFeature, type: "race" })}
                                        startContent={<Icon icon="lucide:users" className="w-4 h-4" />}
                                    >
                                        Racial Trait
                                    </Button>
                                    <Button
                                        size="sm"
                                        color={newFeature.type === "background" ? "secondary" : "default"}
                                        variant={newFeature.type === "background" ? "solid" : "bordered"}
                                        onPress={() => setNewFeature({ ...newFeature, type: "background" })}
                                        startContent={<Icon icon="lucide:book" className="w-4 h-4" />}
                                    >
                                        Background
                                    </Button>
                                    <Button
                                        size="sm"
                                        color={newFeature.type === "feat" ? "warning" : "default"}
                                        variant={newFeature.type === "feat" ? "solid" : "bordered"}
                                        onPress={() => setNewFeature({ ...newFeature, type: "feat" })}
                                        startContent={<Icon icon="lucide:star" className="w-4 h-4" />}
                                    >
                                        Feat
                                    </Button>
                                </div>
                            </div>
                        </div>
                        <Textarea
                            label="Description"
                            placeholder="Enter feature description"
                            value={newFeature.description}
                            onValueChange={(value) => setNewFeature({ ...newFeature, description: value })}
                            className="mt-2"
                        />
                        <div className="flex justify-end mt-4">
                            <Button
                                color="primary"
                                onPress={handleAddFeature}
                            >
                                Add Feature
                            </Button>
                        </div>
                    </CardBody>
                </Card>
            )}

            <div className="space-y-4">
                {filteredFeatures.map((feature) => (
                    <Card key={feature.id} className="overflow-hidden">
                        <CardBody className="p-0">
                            <div
                                className="p-4 flex justify-between items-center cursor-pointer"
                                onClick={() => toggleExpandFeature(feature.id)}
                            >
                                <div className="flex items-center gap-3">
                                    <div className={`flex items-center justify-center w-8 h-8 rounded-full bg-${getTypeColor(feature.type)}-100 dark:bg-${getTypeColor(feature.type)}-900/30`}>
                                        <Icon icon={getTypeIcon(feature.type)} className={`w-4 h-4 text-${getTypeColor(feature.type)}-500`} />
                                    </div>
                                    <div>
                                        <h3 className="text-lg font-medium">{feature.name}</h3>
                                        <div className="text-xs text-slate-500 dark:text-slate-400">
                                            <span>{feature.source}</span>
                                        </div>
                                    </div>
                                </div>
                                <div className="flex items-center gap-2">
                                    <Button
                                        isIconOnly
                                        size="sm"
                                        color="danger"
                                        variant="light"
                                    >
                                        <Icon icon="lucide:trash-2" className="w-4 h-4" />
                                    </Button>
                                    <Icon
                                        icon={expandedFeatureId === feature.id ? "lucide:chevron-up" : "lucide:chevron-down"}
                                        className="w-5 h-5 text-slate-400"
                                    />
                                </div>
                            </div>
                            {expandedFeatureId === feature.id && (
                                <div className="p-4 pt-0 border-t border-slate-200 dark:border-slate-700">
                                    <p className="text-slate-600 dark:text-slate-300 text-sm">{feature.description}</p>
                                </div>
                            )}
                        </CardBody>
                    </Card>
                ))}
            </div>
        </div>
    );
}